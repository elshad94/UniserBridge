using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

[HubName("notificationHub")]
public class NotificationHub : Hub
{
    conn con = new conn();
    login lg = new login();
    private static readonly ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

    #region Methods
    /// <summary>
    /// Provides the handler for SignalR OnConnected event
    /// supports async threading
    /// </summary>
    /// <returns></returns>
    public override Task OnConnected()
    {
     //   Context.Request.Cookies["ref"] = "1";
        var U_ID = Context.QueryString["UID"];
        string profileId = Users.ToString();
        string connectionId = Context.ConnectionId;
        var user = Users.GetOrAdd(profileId, _ => new User
        {
            ProfileId = profileId,
            ConnectionIds = new HashSet<string>()
        });
        lock (user.ConnectionIds)
        {
            user.ConnectionIds.Add(connectionId);
            Groups.Add(connectionId, user.ProfileId);
        }
        string sql = @"Update  [T_SYS_LOG] set  SL_CONID='" + connectionId + "' where SL_DATE in (select MAX(SL_DATE) from [T_SYS_LOG] where SL_U_ID=" + U_ID + @");";
        con.dbrun(sql);



        using (var connection = new SqlConnection(con.Connection_string))
        {

            string query = @"Update [T_SYS_NOTIFICATIONS]  set  N_CONNID='" + connectionId + @"' 
            where  N_CONNID<>'" + connectionId + "' AND  N_STATUS=1 and N_U_ID=" + U_ID;
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
            }

        }       

        return base.OnConnected();
    }

    /// <summary>
    /// Provides the handler for SignalR OnDisconnected event
    /// supports async threading
    /// </summary>
    /// <returns></returns>
    public override Task OnDisconnected()
    {
        string profileId = "111";
        string connectionId = Context.ConnectionId;
        User user;
        Users.TryGetValue(profileId, out user);
        if (user != null)
        {
            lock (user.ConnectionIds)
            {
                user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                Groups.Remove(connectionId, user.ProfileId);
                if (!user.ConnectionIds.Any())
                {
                    User removedUser;
                    Users.TryRemove(profileId, out removedUser);
                }
            }
        }
        return base.OnDisconnected();
    }

    /// <summary>
    /// Provides the handler for SignalR OnReconnected event
    /// supports async threading
    /// </summary>
    /// <returns></returns>
    public override Task OnReconnected()
    {
        return base.OnReconnected();
    }

    /// <summary>
    /// Provides the facility to send individual user notification message
    /// </summary>
    /// <param name="profileId">
    /// Set to the ProfileId of user who will receive the notification
    /// </param>
    /// <param name="message">
    /// set to the notification message
    /// </param>
    public void Send(string profileId, string message)
    {
        Clients.User(profileId).send(message);
    }

    /// <summary>
    /// Provides the facility to send group notification message
    /// </summary>
    /// <param name="username">
    /// set to the user groupd name who will receive the message
    /// </param>
    /// <param name="message">
    /// set to the notification message
    /// </param>
    public void SendUserMessage(String username, String message)
    {
        Clients.Group(username).sendUserMessage(message);
    }

    /// <summary>
    /// Provides the ability to get User from the dictionary for passed in profileId
    /// </summary>
    /// <param name="profileId">
    /// set to the profileId of user that need to be fetched from the dictionary
    /// </param>
    /// <returns>
    /// return User object if found otherwise returns null
    /// </returns>
    private User GetUser(string profileId)
    {
        User user;
        Users.TryGetValue(profileId, out user);
        return user;
    }

    /// <summary>
    /// Provide theability to get currently connected user
    /// </summary>
    /// <returns>
    /// profileId of user based on current connectionId
    /// </returns>
    public IEnumerable<string> GetConnectedUser()
    {
        return Users.Where(x =>
        {
            lock (x.Value.ConnectionIds)
            {
                return !x.Value.ConnectionIds.Contains(Context.ConnectionId, StringComparer.InvariantCultureIgnoreCase);
            }
        }).Select(x => x.Key);
    }
    #endregion

 
    string natifcation = "";
    Int32 natiftacion_count = 0;
    string totalNewCircles = "";
    Int16 totalNewNotification = 0;
    Int16 totalNewJobs = 0;
    [HubMethodName("sendNotifications")]
    public void SendNotifications()
    {
        IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            using (var connection = new SqlConnection(con.Connection_string))
            {
             
                    string query = @"SELECT * FROM (
                                    Select N_DOC_NO,N_NR_ID,NR_S_ID,NR_TEXT1,N_U_ID,NR_URL, N_DOC_ID,  isnull(N_CONNID,0)  N_CONNID,CLC_ALLNAME,N_STATUS,N_DATE from [dbo].[T_SYS_NOTIFICATIONS]
                                    left join [dbo].[TBL_TRANSORDERS] on ORD_RECNO=N_DOC_ID
                                    inner join [dbo].[T_SYS_NATIFCATIONRULE] on NR_ID=N_NR_ID
                                    left JOIN TBL_TRANSORDERS_ONLINE on N_DOC_ID=ORD_O_ID
                                    join TBL_CLCARDS on CLC_RECNO=ORD_CLCRECNO
                                    UNION ALL
                                    Select N_DOC_NO,N_NR_ID,NR_S_ID,NR_TEXT1,N_U_ID,NR_URL, N_DOC_ID,  isnull(N_CONNID,0)  N_CONNID,CLC_ALLNAME,N_STATUS,N_DATE from [dbo].[T_SYS_NOTIFICATIONS]
                                    INNER JOIN TBL_TRANSORDERS_ONLINE on O_ID=N_DOC_ID
                                    inner join [dbo].[T_SYS_NATIFCATIONRULE] on NR_ID=N_NR_ID
                                    join TBL_CLCARDS on CLC_RECNO=O_CLC_RECNO ) dt
                                    where dt.N_STATUS=1 --and isnull(ORD_B_ID,1)=1  
                                    order by N_U_ID,N_DATE desc";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            command.Notification = null;
                            DataTable dt = new DataTable();
                            SqlDependency dependency = new SqlDependency(command);
                            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                            if (connection.State == ConnectionState.Closed)
                                connection.Open();
                            var reader = command.ExecuteReader();
                            dt.Load(reader);
                            string[] natifcation = new string[dt.Rows.Count];
                            string[] natifcation_conn = new string[dt.Rows.Count];
                            string Connection = "";
                            string temp = "";
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string[] words = dt.Rows[i]["NR_TEXT1"].ToString().Split('/');
                                string text1 = "";
                                string text2 = "";

                                text1 = words[0];
                                text2 = words[1];

                                string no = text1.Replace("#NO#", dt.Rows[i]["N_DOC_NO"].ToString()) + " " + text2.Replace("#CLC#", dt.Rows[i]["CLC_ALLNAME"].ToString()) + "##" + dt.Rows[i]["NR_URL"].ToString().Trim() + dt.Rows[i]["N_DOC_ID"].ToString();
                                Connection = dt.Rows[i]["N_CONNID"].ToString().Trim();
                                natifcation_conn[i] = dt.Rows[i]["N_U_ID"].ToString();
                                natifcation[i] = no;
                                //if (Connection == temp || i == 0)
                                //{
                                //    temp = dt.Rows[i]["N_CONNID"].ToString().Trim();
                                //    natifcation[i] = no;

                                //    //  context.Clients.Client(Connection).RecieveNotification(natifcation);
                                //}
                                //else
                                //{
                                //    Array.Clear(natifcation, 0, natifcation.Length);
                                //    temp = dt.Rows[i]["N_CONNID"].ToString().Trim();
                                //    natifcation[i] = no;
                                //}
                            }
                           // context.Clients.Client(Connection).RecieveNotification(natifcation, natifcation_conn);

                            context.Clients.All.RecieveNotification(natifcation, natifcation_conn);

                            connection.Close();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
               
            }          
         //   return null;
   }


    private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
    {
        if (e.Type == SqlNotificationType.Change)
        {
            NotificationHub nHub = new NotificationHub();
            nHub.SendNotifications();
        }
    }

    public void ReadNatifcation(int U_TYPE, int DOC_ID)
    { 
        string query = "";

        if (DOC_ID == -1)
        {
            query = @"DELETE t FROM  T_SYS_NOTIFICATIONS t INNER JOIN T_SYS_USER on U_ID = t.N_U_ID WHERE U_TYPE =" + U_TYPE;
        } 
        else
            {
            //query = @"
            //                UPDATE t
            //                SET t.N_STATUS = 0
            //                FROM T_SYS_NOTIFICATIONS t
            //                INNER JOIN T_SYS_USER on U_ID = N_U_ID
            //                WHERE U_TYPE = " + U_TYPE + " AND N_DOC_ID = " + DOC_ID;
            query = @"DELETE  FROM  T_SYS_NOTIFICATIONS  WHERE N_U_ID =" + U_TYPE;
        }
       

        //query += @"Update T_SYS_NOTIFCATIONMAILS set NM_STATUS=0 where  NM_U_ID='" + U_ID + "' and NM_DOC_ID='" + DOC_ID + "'";

       
        con.dbrun(query);
    }
}
