using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Mail
/// </summary>
public class Mail
{
    login lgn = new login();

    public class OMail
    {
        private string N_subject;

        public string subject
        {
            get { return N_subject; }
            set { N_subject = value; }
        }
        private string N_message;

        public string message
        {
            get { return N_message; }
            set { N_message = value; }
        }
    }

    public void send_mail(string to, string from, string subject, string message, string Attach)
    {
        MailMessage msgs = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        int tls12 = 3072; 
        System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)tls12;
        string msg = string.Empty;
        try
        {
            using (MailMessage mm = new MailMessage("no-reply@adycontainer.com", to))
            {
                mm.Subject = subject;
                mm.Body = @"<body style=""margin: 0; padding: 0;"">
                             <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""20%"">
                              <tr>
                              <td> </td><td>" + from + @"</td>
                            </tr>
                             </table></br>

                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                             <tr>
                              <td>" + message + @"</td>
                            </tr>
                             </table>
                         </body>";

                if (Attach != "")
                {


                    System.Net.Mail.Attachment attachment;
                    string apPath = System.Web.Hosting.HostingEnvironment.MapPath(Attach);
                    attachment = new System.Net.Mail.Attachment(apPath);
                    mm.Attachments.Add(attachment);
                }

                mm.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "mail.uniser.com";
                //smtp.EnableSsl = false;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("system_info@uniser.az", "Ss123456");
                //smtp.Port = 25;
                //smtp.Send(mm);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.office365.com";
                // smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("no-reply@adycontainer.com", "NDzhv489");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                //smtp.Port = 25;
                smtp.Send(mm);
                //Set_MailLog("elshad@uniser.az", to, subject, message, CC, Attach, Type);
            }
        }
        catch (Exception ex) { string AA = ex.ToString(); }
    }

    public void send_mailRate(string to, string from, string subject, string message)
    {
        //using (MailMessage mm = new MailMessage("no-reply@adycontainer.com", to))
        //{

        using (MailMessage mm = new MailMessage("no-reply@alliancemultimodal.com", to))//item["m_u_email"].tostring()///
        {
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            int tls12 = 3072; // Tls12 is not defined in the SecurityProtocolType enum in CLI/C++ / ToolsVersion="4.0"  
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)tls12;
            mm.Subject = subject;
            mm.Body = @"<body style=""margin: 0; padding: 0;"">
                              <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                               <tr>
                                <td>" + message + @"</td>
                              </tr>
                               </table>
                           </body>";

            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("no-reply@alliancemultimodal.com", "Kum61874");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            //smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(mm);
            //}

            //  mm.Subject = subject; 
            //  mm.Body = @"<body style=""margin: 0; padding: 0;"">
            //                  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
            //                   <tr>
            //                    <td>" + message + @"</td>
            //                  </tr>
            //                   </table>
            //               </body>";
            ////mm.Bcc.Add("");
            ////mm.Bcc.Add(" ");
            //  mm.IsBodyHtml = true;


            //  SmtpClient smtp = new SmtpClient();
            //  smtp.Host = "smtp.office365.com";
            //  smtp.EnableSsl = true;
            //  NetworkCredential NetworkCred = new NetworkCredential("no-reply@adycontainer.com", "NDzhv489");
            //  smtp.UseDefaultCredentials = true;
            //  smtp.Credentials = NetworkCred;


            //  smtp.Port = 587;
            //  smtp.Send(mm);

            con.dbrun(@"insert into T_SYS_MAILLOG (ML_DATE, ML_TO, ML_FROM, ML_SUBJECT, ML_MESSAGE, ML_TYPE)
            values
            (GETDATE(), N'" + to + "', '" + from + "', N'" + mm.Subject + "', N'" + mm.Body + "', 'send_mailRate')");
        //send_mail("elshad@uniser.az", "RATE", "Error yemedim", message);


        }
    }

    conn con = new conn();
    public List<OMail> Get_Notification(int Notification_id, int Notification_lang)
    {
        List<OMail> List_data = new List<OMail>();
        string query = "";
        query = @"Select N_ID,N_SUBJECT" + Notification_lang + @" SUBJECT,
                  N_MESSAGE" + Notification_lang + " MESSAGE from [dbo].[T_SYS_NOTIFICATIONS] where N_ID=1";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@Notification_id", Notification_id);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new OMail
            {
                subject = reader["SUBJECT"].ToString(),
                message = reader["MESSAGE"].ToString()
            });
        }
        return List_data;
    }

    public void send_gmail(string to, string from, string subject, string message)
    {
        using (MailMessage mm = new MailMessage("", to)) //imsart@uniser.az
        {
            mm.Subject = subject;
            mm.Body = @"<body style=""margin: 0; padding: 0;"">
                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""20%"">
                        <tr>
                        <td>From:</td><td>" + from + @"</td>
                        </tr>
                        </table></br>
                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                        <tr>
                        <td>" + message + @"</td>
                        </tr>
                        </table>
                        </body>";

            if (subject == "Tarif Kalkulyatoru")
            {
                mm.CC.Add("");
                mm.CC.Add("");
                mm.CC.Add("");
            }
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.uniser.az";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("system_info@uniser.az", "Ss123456");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            // smtp.Send(mm);
        }
    }
}