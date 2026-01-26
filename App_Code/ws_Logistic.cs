using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for ws_Logistic
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ws_Logistic : System.Web.Services.WebService
{
    login lg = new login();
    conn con = new conn();
    system sys = new system();
    System.Data.SqlClient.SqlDataReader reader = null;
    System.Data.SqlClient.SqlDataReader reader2 = null;

    public ws_Logistic()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    #region Report

    [WebMethod(EnableSession = true)]
    public void GetLogisticReport()
    {
        string sql = @"EXEC SP_LOGISTIC";



        reader = con.dbrun(sql);
        List<ReportData> reportDatas = new List<ReportData>();
        ReportMainData reportMainData = new ReportMainData();

        while (reader.Read())
        {
            List<string> countries = new List<string>();
            List<string> contents = new List<string>();
            ReportData reportData = new ReportData();
            contents.Add(reader["AREA"].ToString());



            for (int i = 1; i < reader.FieldCount - 1; i++)
            {
                string fieldName = "";
                try
                {
                    fieldName = reader.GetName(i);
                    countries.Add(fieldName);


                    if (reader[fieldName].ToString() != "")
                    {
                        foreach (string count in reader[fieldName].ToString().Replace(" ", "").Split('/'))
                        {
                            contents.Add(count);
                        }

                    }
                    else
                    {
                        contents.Add("0");
                        contents.Add("0");
                        contents.Add("0");
                    }
                }
                catch (Exception)
                { }
            }
            contents.Add(reader["SMCONT"].ToString());

            reportData.contents = contents;
            reportMainData.countries = countries;
            reportDatas.Add(reportData);
        }

        reportMainData.reportDatas = reportDatas;

        JavaScriptSerializer jss = new JavaScriptSerializer();

        Context.Response.Write(jss.Serialize(reportMainData));
    }

    #endregion

    [WebMethod(EnableSession = true)]
    public void GetAutoComplate(string text, int type)
    {
        string sql = "";
        if (type == 1)
        {
            sql = @"SELECT TOP 5 ORD_RECNO Id,ORD_FICHENO Name FROM TBL_TRANSORDERS
                        WHERE ORD_FICHENO LIKE '%" + text + @"%'
                        ORDER BY ORD_RECNO DESC";
        }
        else if (type == 2)
        {
            sql = @"SELECT TOP 5 ORD_RECNO Id, ORD_PODCODE Name FROM TBL_TRANSORDERS
                        WHERE ORD_PODCODE LIKE '%" + text + @"%'
                        ORDER BY ORD_RECNO DESC";
        }
        else if (type == 3)
        {
            sql = @"SELECT TOP 5 * FROM(
		                SELECT TN_ID Id,TN_PREFNO+TN_NO Name FROM TBL_TRANSPORTPARK
					    WHERE TN_TYPE = 2	)
					AS DATA WHERE Name like '%" + text + "%' ";
        }
        else if (type == 4)
        {
            sql = @"SELECT TOP 5 
		                PNT_RECNO ID ,PNT_CODE+ ' - ' +PNT_NAME NAME FROM TBL_POINTS
						WHERE ISNULL(PNT_STATUS,0) <> -1 AND PNT_CODE+ ' - ' +PNT_NAME LIKE N'%" + text + "%' order by NAME";
        }
        else if (type == 5)
        {
            sql = @"SELECT TOP 5 id ID, name" + lg.Get_Lang() + @" NAME FROM T_COUNTRIES
			        WHERE id <> 0 AND ISNULL(status,0) <> -1 and name" + lg.Get_Lang() + @" like N'" + text + @"%' 
                    ORDER BY NAME";
        }
        else if (type == 6) //QNQ (YHN)
        {
            sql = @"Select top 10 * from 
                    (SELECT isnull(GNG.STC_ID,-1) Id, GNG.STC_CODE + ' - ' + GNG.STC_NAME Name
                    FROM (SELECT STC_ID, STC_CODE, STC_NAME FROM TBL_STCARDS WHERE STC_STTYPE=1 AND STC_STATUS=0) GNG
                    LEFT JOIN TBL_STJCARDS GE ON GE.STCJ_GNG_CODE=GNG.STC_CODE
                    UNION ALL
                    Select STC_ID Id, STC_CODE + ' - ' + STC_NAME Name FROM TBL_STCARDS WHERE STC_STTYPE=3 AND STC_STATUS=0
                    ) TBL
                    where Name like N'%" + text + "%' Order by Name";
        }

        reader = con.dbrun(sql);
        List<AutoComplateModel> clsList = new List<AutoComplateModel>();
        try
        {
            while (reader.Read())
            {
                AutoComplateModel cls = new AutoComplateModel();
                //AutoComplateModel autocomplateResult = sys.SqlReaderToModel<AutoComplateModel>(cls, reader);
                cls.Id = int.Parse(reader["Id"].ToString());
                cls.Name = reader["Name"].ToString();
                clsList.Add(cls);
            }
            reader.Close();
        }
        catch (Exception)
        {

        }
        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(clsList));
    }

    [WebMethod]
    public void GetTransportList(string Ficheno, string Podcode, string FullEmpty, string RTID)
    {
        string sql = "";
        string filter = "";

        GetDataModel dataModel = new GetDataModel();
        List<GetTransportModel> trpList = new List<GetTransportModel>();

        GetOrderModel ordcls = new GetOrderModel();
        GetRouteModel routecls = new GetRouteModel();


        if (RTID != "0")
        {
            try
            {
                reader = con.dbrun(@"SELECT ORD_FICHENO FROM TBL_ROUTE
					                INNER JOIN TBL_TRANSORDERS ON RT_ORDID = ORD_RECNO
					                WHERE RT_STATUS <> -1 AND RT_AREA <> 0 and RT_ID =" + RTID);

                if (reader.Read())
                {
                    Ficheno = reader["ORD_FICHENO"].ToString();
                }

                if (Ficheno.Trim() != "undefined" && Ficheno.Trim() != null)
                {
                    filter = @" AND ORD_FICHENO = '" + Ficheno + "'";
                }
                else
                {
                    filter = @" AND ORD_PODCODE = '" + Podcode + "'";
                }

                reader = con.dbrun(@"SELECT * FROM TBL_ROUTELINE
					            INNER JOIN TBL_ROUTE ON RT_ID = RTL_RTID
					            INNER JOIN TBL_TRANSORDERS ON RT_ORDID = ORD_RECNO
					            WHERE RTL_STATUS <> -1 AND RT_AREA <> 0 AND RT_ID =" + RTID + filter);

                if (reader.Read())
                {
                    sql = @"
                            SELECT 
                            0 TRN_ID,
                            RTL_WAGONID WagonId,
                            RTL_WAGONNO WagonNo,
							RTL_CONTAINERID ContainerId,
                            RTL_CONTAINERNO ContainerNo,

                            RTL_OVERHEAD Overhead,
							RT_NAME" + lg.Get_Lang() + @" ContCat,
							SP1.SC_VALUE" + lg.Get_Lang() + @" ContType,
							ISNULL(RTL_ID,0) RtlId,
							ISNULL(RTL_RTID,0) RtId,
							ISNULL(RTL_UPFID,0) UpfId,
							ISNULL(RTL_CHECK,0) TR_Check,
                            UF_FILE UFILE

                            FROM TBL_ROUTELINE
							LEFT JOIN TBL_ROUTE ON RTL_RTID = RT_ID
						    INNER JOIN TBL_TRANSORDERS ON ORD_RECNO = RT_ORDID
							LEFT JOIN TBL_UPFILES ON UF_ID = RTL_UPFID
							LEFT JOIN TBL_RTCTYPE RT1 ON RT1.RT_ID= TBL_ROUTELINE.RTL_TRCAT AND RT1.RT_TYPE=2
							LEFT JOIN TBL_SPECODES SP1 ON SP1.SC_REFID = TBL_ROUTELINE.RTL_TRTYPE AND SP1.SC_TYPE = 'CONT_TYPE'
                            
                            WHERE RTL_STATUS <> -1 AND TBL_ROUTE.RT_ID =" + RTID + filter;
                    reader = con.dbrun(sql);
                    while (reader.Read())
                    {
                        GetTransportModel trpcls = new GetTransportModel();
                        trpcls.TRN_ID = int.Parse(reader["TRN_ID"].ToString());
                        trpcls.WagonId = reader["WagonId"].ToString();
                        trpcls.WagonNo = reader["WagonNo"].ToString();
                        trpcls.ContainerId = reader["ContainerId"].ToString();
                        trpcls.ContainerNo = reader["ContainerNo"].ToString();
                        trpcls.Overhead = reader["Overhead"].ToString();
                        trpcls.ContCat = reader["ContCat"].ToString();
                        trpcls.ContType = reader["ContType"].ToString();
                        trpcls.RtlId = reader["RtlId"].ToString();
                        trpcls.RtId = reader["RtId"].ToString();
                        trpcls.UpfId = reader["UpfId"].ToString();
                        trpcls.TR_Check = reader["TR_Check"].ToString();
                        trpcls.File = reader["UFILE"].ToString();


                        trpList.Add(trpcls);
                    }
                    reader.Close();
                }
                else
                {

                }
                ///Sifarish
                string sqlOrder = @"SELECT ORD_RECNO ORDID,ORD_FICHENO OrdFicheno,convert(varchar, ORD_FICHEDATE, 23) FicheDate,ORD_PODCODE OrdPodcode, SPC.SC_VALUE1 Rejim, 
		                ORD_FCLIENT Sender, FPN.PNT_CODE+' - '+FPN.PNT_NAME SenderSt, ORD_TCLIENT Receiver, TPN.PNT_CODE +' - '+TPN.PNT_NAME ReceiverSt,
		                CLC_ALLNAME Client,  STC1.STC_CODE +' - '+ STC1.STC_NAME1 QnqName,BPN.PNT_CODE +' - '+ BPN.PNT_NAME BrdEntry,EPN.PNT_CODE +' - '+ EPN.PNT_NAME BrdExit
		                FROM TBL_TRANSORDERS
		                LEFT JOIN TBL_CLCARDS ON ORD_CLCRECNO = CLC_RECNO
		                LEFT JOIN TBL_SPECODES SPC ON (ORD_ACTTYPE=SPC.SC_REFID AND SC_TYPE='ORD_TYPEACT')
		                LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
                        LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
		                LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
                        LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
		                LEFT JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID AND STC1.STC_STTYPE=1)
                        WHERE  ORD_STATUS <> 99 " + filter;
                reader = con.dbrun(sqlOrder);

                if (reader.Read())
                {
                    ordcls.ORDID = reader["ORDID"].ToString();
                    ordcls.OrdFicheno = reader["OrdFicheno"].ToString();
                    ordcls.FicheDate = reader["FicheDate"].ToString();
                    ordcls.OrdPodcode = reader["OrdPodcode"].ToString();
                    ordcls.Rejim = reader["Rejim"].ToString();
                    ordcls.Sender = reader["Sender"].ToString();
                    ordcls.SenderSt = reader["SenderSt"].ToString();
                    ordcls.Receiver = reader["Receiver"].ToString();
                    ordcls.ReceiverSt = reader["ReceiverSt"].ToString();
                    ordcls.Client = reader["Client"].ToString();
                    ordcls.QnqName = reader["QnqName"].ToString();
                    ordcls.BrdEntry = reader["BrdEntry"].ToString();
                    ordcls.BrdExit = reader["BrdExit"].ToString();

                }
                ///Sifarish End



                //RootData
                reader = con.dbrun(@"
                        SELECT 
                        RT_ID RTID,
                        RT_LoadCountry LoadCountry,
                        RT_DestCountry DestCountry,
                        RT_LOADSTID LoadStId,
                        CASE
							WHEN RT_LOADSTID = 0 THEN RT_LOADSTNAME
							ELSE P_L.PNT_CODE + ' - ' + P_L.PNT_NAME
						END LoadStName,
                        RT_DESTSTID DestStId,
						CASE
							WHEN RT_DESTSTID = 0 THEN RT_DESTSTNAME
							ELSE P_D.PNT_CODE + ' - ' + P_D.PNT_NAME
						END DestStName,
                        RT_Area AreaId,
                        name" + lg.Get_Lang() + @" Area,
                        RT_Port Port,
                        RT_Shipname Shipname,
                        RT_Shiptype Shiptype,
                        RT_FULLEMPTY FullEmpty,
                        RT_EntryDate EntryDate,
                        RT_ExitDate ExitDate
                        --SC_VALUE" + lg.Get_Lang() + @" Shiptype
                        FROM TBL_ROUTE
                        INNER JOIN TBL_TRANSORDERS ON ORD_RECNO = RT_ORDID
                        LEFT JOIN TBL_POINTS P_L ON RT_LOADSTID = P_L.PNT_RECNO
                        LEFT JOIN TBL_POINTS P_D ON RT_DESTSTID = P_D.PNT_RECNO
                        LEFT JOIN T_COUNTRIES ON RT_AREA = id
                        --LEFT JOIN TBL_SPECODES ON SC_REFID = RT_SHIPTYPE AND SC_TYPE = 'SHIPTYPE'

                        WHERE RT_STATUS <> -1 AND RT_ID =" + RTID + filter);
                while (reader.Read())
                {
                    routecls.RTID = int.Parse(reader["RTID"].ToString());
                    routecls.LoadCountry = reader["LoadCountry"].ToString();
                    routecls.DestCountry = reader["DestCountry"].ToString();
                    routecls.LoadStId = int.Parse(reader["LoadStId"].ToString());
                    routecls.LoadStName = reader["LoadStName"].ToString();
                    routecls.DestStId = int.Parse(reader["DestStId"].ToString());
                    routecls.DestStName = reader["DestStName"].ToString();
                    routecls.AreaId = int.Parse(reader["AreaId"].ToString());
                    routecls.Area = reader["Area"].ToString();
                    routecls.Port = reader["Port"].ToString();
                    routecls.Shipname = reader["Shipname"].ToString();
                    routecls.Shiptype = reader["Shiptype"].ToString();
                    routecls.FullEmpty = reader["FullEmpty"].ToString();
                    routecls.EntryDate = reader["EntryDate"].ToString();
                    routecls.ExitDate = reader["ExitDate"].ToString();

                }
                //END ROOTDATA





                dataModel.listTrpModel = trpList;
                dataModel.OrdModel = ordcls;
                dataModel.RouteModel = routecls;

            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            if (Ficheno.Trim() != "undefined" && Ficheno.Trim() != null)
            {
                filter = @" AND ORD_FICHENO = '" + Ficheno + "'";
            }
            else
            {
                filter = @" AND ORD_PODCODE = '" + Podcode + "'";
            }
            sql = @"
                            SELECT DISTINCT TRN_ID,
                            CASE
	                            WHEN TRN_TRTYPE = '1' THEN TRN_TNID
	                            ELSE ''
                            END WagonId,
                            CASE
	                            WHEN TRN_TRTYPE = '1' THEN CAST(TRN_NO as nvarchar(10))
	                            ELSE ''
                            END WagonNo,
							CASE
	                            WHEN TRN_TRTYPE = '2' THEN TRN_TNID
	                            ELSE ''
                            END ContainerId,
                            CASE
	                            WHEN TRN_TRTYPE = '2' THEN TRN_PREFIX + CAST(TRN_NO as nvarchar(10))
	                            ELSE ''
                            END ContainerNo,
                            '' Overhead,
							TRN_TRCAT ContCatId,RT_NAME" + lg.Get_Lang() + @" ContCat,
							TRN_TYPE ContTypeId,SP1.SC_VALUE" + lg.Get_Lang() + @" ContType,
							0 RtlId,
							0 RtId,
							0 UpfId,
							0 TR_Check

                            FROM TBL_TRANSPORTLIST
						    INNER JOIN TBL_TRANSORDERS ON ORD_RECNO = TRN_ORDID
							LEFT JOIN TBL_RTCTYPE ON RT_ID= TRN_TRCAT AND RT_TYPE=2
							LEFT JOIN TBL_SPECODES SP1 ON SP1.SC_REFID = TRN_TYPE AND SP1.SC_TYPE = 'CONT_TYPE'
                            WHERE TRN_FULLEMPTY = " + FullEmpty + " AND TRN_STATUS = 1 " + filter;

            reader = con.dbrun(sql);

            while (reader.Read())
            {
                GetTransportModel trpcls = new GetTransportModel();
                trpcls.TRN_ID = int.Parse(reader["TRN_ID"].ToString());
                trpcls.WagonId = reader["WagonId"].ToString();
                trpcls.WagonNo = reader["WagonNo"].ToString();
                trpcls.ContainerId = reader["ContainerId"].ToString();
                trpcls.ContainerNo = reader["ContainerNo"].ToString();
                trpcls.Overhead = reader["Overhead"].ToString();
                trpcls.ContCatId = reader["ContCatId"].ToString();
                trpcls.ContCat = reader["ContCat"].ToString();
                trpcls.ContTypeId = reader["ContTypeId"].ToString();
                trpcls.ContType = reader["ContType"].ToString();
                trpcls.RtlId = reader["RtlId"].ToString();
                trpcls.RtId = reader["RtId"].ToString();
                trpcls.UpfId = reader["UpfId"].ToString();
                trpcls.TR_Check = reader["TR_Check"].ToString();



                trpList.Add(trpcls);
            }
            reader.Close();


            string sqlOrder = @"SELECT ORD_FICHENO OrdFicheno,convert(varchar, ORD_FICHEDATE, 23) FicheDate,ORD_PODCODE OrdPodcode, SPC.SC_VALUE1 Rejim, 
		    ORD_FCLIENT Sender, FPN.PNT_CODE+' - '+FPN.PNT_NAME SenderSt, ORD_TCLIENT Receiver, TPN.PNT_CODE +' - '+TPN.PNT_NAME ReceiverSt,
		    CLC_ALLNAME Client,  STC1.STC_CODE +' - '+ STC1.STC_NAME1 QnqName,BPN.PNT_CODE +' - '+ BPN.PNT_NAME BrdEntry,EPN.PNT_CODE +' - '+ EPN.PNT_NAME BrdExit
		    FROM TBL_TRANSORDERS
		    LEFT JOIN TBL_CLCARDS ON ORD_CLCRECNO = CLC_RECNO
		    LEFT JOIN TBL_SPECODES SPC ON (ORD_ACTTYPE=SPC.SC_REFID AND SC_TYPE='ORD_TYPEACT')
		    LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
            LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
		    LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
            LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
		    LEFT JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID AND STC1.STC_STTYPE=1)
            WHERE  ORD_STATUS <> 99 " + filter;


            reader = con.dbrun(sqlOrder);

            if (reader.Read())
            {
                ordcls.OrdFicheno = reader["OrdFicheno"].ToString();
                ordcls.FicheDate = reader["FicheDate"].ToString();
                ordcls.OrdPodcode = reader["OrdPodcode"].ToString();
                ordcls.Rejim = reader["Rejim"].ToString();
                ordcls.Sender = reader["Sender"].ToString();
                ordcls.SenderSt = reader["SenderSt"].ToString();
                ordcls.Receiver = reader["Receiver"].ToString();
                ordcls.ReceiverSt = reader["ReceiverSt"].ToString();
                ordcls.Client = reader["Client"].ToString();
                ordcls.QnqName = reader["QnqName"].ToString();
                ordcls.BrdEntry = reader["BrdEntry"].ToString();
                ordcls.BrdExit = reader["BrdExit"].ToString();

            }

            dataModel.listTrpModel = trpList;
            dataModel.OrdModel = ordcls;



        }







        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(dataModel));

    }

    [WebMethod]
    public void LoadCombo(int op)
    {
        System.Data.SqlClient.SqlDataReader reader = null;
        List<ComboModel> clsList = new List<ComboModel>();
        reader = con.dbrun(GetQuery(op));

        try
        {
            while (reader.Read())
            {
                ComboModel cls = new ComboModel();
                cls.ID = reader["ID"].ToString();
                cls.NAME = reader["NAME"].ToString();
                clsList.Add(cls);
            }
        }
        catch (Exception)
        {

        }

        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(clsList));
    }


    [WebMethod(EnableSession = true)]
    public void LoadComboType(int trType)
    {
        string sql = "";
        System.Data.SqlClient.SqlDataReader reader = null;

        List<ComboModel> clsList = new List<ComboModel>();
        if (trType == 1)
        {
            sql = @"SELECT VT_ID ID, VT_NAME" + lg.Get_Lang().ToString() + @" NAME FROM TBL_VAGONTYPE WHERE  ISNULL(VT_STATUS, 0) <> -1"; //VT_C_ID = "+ category +" AND;
        }
        else
        {
            sql = @"SELECT SC_REFID ID, SC_VALUE1 NAME FROM TBL_SPECODES WHERE SC_TYPE = 'CONT_TYPE' AND ISNULL(SC_STATUS,0) <> -1";// AND  SC_PARENT = " + category;
        }
        reader = con.dbrun(sql);

        try
        {
            while (reader.Read())
            {
                ComboModel cls = new ComboModel();
                cls.ID = reader["ID"].ToString();
                cls.NAME = reader["NAME"].ToString();
                clsList.Add(cls);
            }
        }
        catch (Exception)
        {

        }

        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(clsList));
    }


    private string GetQuery(int op)
    {
        string query = "";
        switch (op)
        {
            case 1: //olkə
                query = @"SELECT id ID, name" + lg.Get_Lang() + @" NAME FROM T_COUNTRIES
			             WHERE id <> 0 AND ISNULL(status,0) <> -1
                         ORDER BY name" + lg.Get_Lang();
                break;
            case 2: //shiptype
                query = @"SELECT SC_REFID ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE SC_TYPE = 'EMPTY'
                         UNION 
                         SELECT SC_REFID ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE SC_TYPE = 'SHIPTYPE' AND SC_STATUS <> -1";
                break;
            case 3: //contCat
                query = @"SELECT RT_ID ID, RT_NAME" + lg.Get_Lang().ToString() + @" NAME FROM TBL_RTCTYPE WHERE RT_TYPE=2";
                break;
            case 4: //Full/Empty
                query = @"SELECT SC_REFID ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES
                          WHERE ISNULL(SC_STATUS,0) = 0 AND SC_TYPE = 'EXP_TYPE'";
                break;

        }
        return query;
    }





    [WebMethod(EnableSession = true)]
    public void InsertData(string RTID, string routeData, string rtlData, string rtlFullEmtyp)
    {
        string sql = "";
        String sql_rtl = "";
        SaveModel svModel = JsonConvert.DeserializeObject<SaveModel>(routeData);
        List<SaveLineModel> svLineModel = JsonConvert.DeserializeObject<List<SaveLineModel>>(rtlData);

        if (RTID == "0")
        {
            sql = @"INSERT INTO TBL_ROUTE(RT_ORDID,RT_LOADCOUNTRY,RT_LOADSTID,RT_LOADSTNAME,RT_DESTCOUNTRY,RT_DESTSTID,RT_DESTSTNAME,RT_AREA,RT_PORT,RT_SHIPNAME,RT_SHIPTYPE,RT_FULLEMPTY,RT_ENTRYDATE,RT_EXITDATE)
					VALUES
					(
                        " + svModel.ORDID + @",
                        " + svModel.LoadCountry + @",
                        " + svModel.LoadStId + @",
                        '" + svModel.LoadStName + @"',
                        " + svModel.DestCountry + @",
                        " + svModel.DestStId + @",
                        '" + svModel.DestStName + @"',
                        " + svModel.AreaId + @",
                        N'" + svModel.Port + @"',
                        N'" + svModel.Shipname + @"',
                        " + svModel.Shiptype + @",
                        " + rtlFullEmtyp + @",
                        '" + svModel.EntryDate + @"',
                        '" + svModel.ExitDate + @"'
					); SELECT SCOPE_IDENTITY() RT_ID;";

            reader = con.dbrun(sql);
            if (reader.Read())
            {
                foreach (var itemRtl in svLineModel)
                {

                    if (itemRtl.File != null && itemRtl.File != "")
                    {
                        MoveFile(itemRtl.File);

                        sql = @" INSERT INTO TBL_UPFILES(UF_TYPE, UF_APP, UF_FILE, UF_UFILE, UF_U_ID, UF_STATUS)
                                VALUES
                                (
                                1,
                                'LOGISTIC',
                                '" + itemRtl.File.Replace("LogisticUploadsTemp", "LogisticUploads") + @"',
                                '" + itemRtl.File.Remove(0, 39) + @"',
                                " + lg.Get_userid() + @",
                                1
                                ); SELECT SCOPE_IDENTITY() UF_ID";
                        reader2 = con.dbrun(sql);
                        if (reader2.Read())
                        {
                            itemRtl.UpfId = int.Parse(reader2["UF_ID"].ToString());
                        }

                    }

                    sql_rtl = @"
                    INSERT INTO TBL_ROUTELINE(RTL_RTID, RTL_WAGONID, RTL_WAGONNO, RTL_CONTAINERID, RTL_CONTAINERNO, RTL_OVERHEAD, RTL_TRCAT, RTL_TRTYPE, RTL_FULLEMPTY, RTL_UPFID, RTL_CHECK)
                    VALUES(
                        " + reader["RT_ID"].ToString() + @",
                        " + itemRtl.WagonId + @",
                        '" + itemRtl.WagonNo + @"',
                        " + itemRtl.ContainerId + @",
                        '" + itemRtl.ContainerNo + @"',
                        '" + itemRtl.Overhead + @"',
                        " + itemRtl.ContCatId + @",
                        " + itemRtl.ContTypeId + @",
                        " + rtlFullEmtyp + @",
                        " + itemRtl.UpfId + @",
                        '" + itemRtl.TR_Check + @"'

                    )";
                    con.dbrun(sql_rtl);
                }
            }

        }
        //   else
        //   {
        //       sql = @"
        //               UPDATE TBL_ROUTE
        //SET 
        //                   RT_ORDID       = " + svModel.ORDID + @",
        //                   RT_LOADCOUNTRY = " + svModel.LoadCountry + @",
        //                   RT_LOADST      = " + svModel.LoadStId + @",
        //                   RT_DESTCOUNTRY = " + svModel.DestCountry + @",
        //                   RT_DESTST      = " + svModel.DestStId + @",
        //                   RT_AREA        = '" + svModel.Area + @"',
        //                   RT_PORT        = '" + svModel.Port + @"',
        //                   RT_SHIPNAME    = '" + svModel.Shipname + @"',
        //                   RT_SHIPTYPE    = " + svModel.Shiptype + @",
        //                   RT_ENTRYDATE   = '" + svModel.EntryDate + @"',
        //                   RT_EXITDATE    = '" + svModel.ExitDate + @"'
        //                   WHERE RT_ID = " + RTID;

        //   }


        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write("Success");
    }

    [WebMethod(EnableSession = true)]
    public void InsertRouteLine(string RTID, string rtlData)
    {
        SaveLineModel rtlModel = JsonConvert.DeserializeObject<SaveLineModel>(rtlData); //lineModel
        try
        {
            if (rtlModel.File != null && rtlModel.File != "")
            {
                string sql = @"INSERT INTO TBL_UPFILES(UF_TYPE, UF_APP, UF_FILE, UF_UFILE, UF_U_ID, UF_STATUS)
                                VALUES
                                (
                                1,
                                'LOGISTIC',
                                '" + rtlModel.File + @"',
                                '" + rtlModel.File.Remove(0, 39) + @"',
                                " + lg.Get_userid() + @",
                                1
                                ); SELECT SCOPE_IDENTITY() UF_ID";
                reader2 = con.dbrun(sql);
                if (reader2.Read())
                {
                    rtlModel.UpfId = int.Parse(reader2["UF_ID"].ToString());
                }

                //MoveFile(rtlModel.File);
            }


            string sql_rtl = @"
                    INSERT INTO TBL_ROUTELINE(RTL_RTID, RTL_WAGONID, RTL_WAGONNO, RTL_CONTAINERID, RTL_CONTAINERNO, RTL_OVERHEAD,RTL_TRCAT, RTL_TRTYPE, RTL_UPFID, RTL_CHECK)
                    VALUES(
                        " + RTID + @",
                        " + rtlModel.WagonId + @",
                        '" + rtlModel.WagonNo + @"',
                        " + rtlModel.ContainerId + @",
                        '" + rtlModel.ContainerNo + @"',
                        '" + rtlModel.Overhead + @"',
                        " + rtlModel.ContCatId + @",
                        " + rtlModel.ContTypeId + @",
                        " + rtlModel.UpfId + @",
                        '" + rtlModel.TR_Check + @"'

                    )";
            con.dbrun(sql_rtl);

        }
        catch (Exception ex)
        {

            throw;
        }

    }

    [WebMethod]
    public void UpdateRtlCheck(string RTLID, string check)
    {
        try
        {
            string sql = @"
                            UPDATE TBL_ROUTELINE
						    SET RTL_CHECK =" + check + @"
						    WHERE RTL_ID = " + RTLID;
            con.dbrun(sql);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [WebMethod]
    public void UpdateRoute(string routeData)
    {
        SaveModel routeModel = JsonConvert.DeserializeObject<SaveModel>(routeData);
        try
        {
            string sql = @"  
                    UPDATE TBL_ROUTE

                    SET
                        RT_LOADCOUNTRY = " + routeModel.LoadCountry + @",
                        RT_LOADSTID = " + routeModel.LoadStId + @",
                        RT_LOADSTNAME = '" + routeModel.LoadStName + @"',
                        RT_DESTCOUNTRY = " + routeModel.DestCountry + @",
                        RT_DESTSTID = " + routeModel.DestStId + @",
                        RT_DESTSTNAME = '" + routeModel.DestStName + @"',
                        RT_AREA = '" + routeModel.AreaId + @"',
                        RT_PORT = N'" + routeModel.Port + @"',
                        RT_SHIPNAME = N'" + routeModel.Shipname + @"',
                        RT_SHIPTYPE = " + routeModel.Shiptype + @",
                        RT_ENTRYDATE = '" + routeModel.EntryDate + @"',
                        RT_EXITDATE = '" + routeModel.ExitDate + @"'
                        WHERE RT_ID = " + routeModel.RTID;
            con.dbrun(sql);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    [WebMethod]
    public void DeleteRouteLine(string RTLID, string filePath)
    {
        try
        {
            if (filePath != null && filePath != "")
            {
                System.IO.File.Delete(Server.MapPath(filePath));
                reader = con.dbrun(@"
	                            SELECT RTL_UPFID FROM TBL_ROUTELINE
						        WHERE RTL_ID =" + RTLID);
                if (reader.Read())
                {
                    con.dbrun(@"DELETE TBL_UPFILES
						    WHERE UF_ID = " + reader["RTL_UPFID"].ToString());
                }
            }

            con.dbrun(@"UPDATE TBL_ROUTELINE
                        SET RTL_STATUS = -1
                        WHERE RTL_ID = " + RTLID);

        }
        catch (Exception)
        {

            throw;
        }


    }

    private void MoveFile(string oldPath)
    {
        conn con = new conn();
        DateTime dateTime = DateTime.Now;

        string oldpath2 = Server.MapPath(oldPath);
        string newPath2 = Server.MapPath(Path.Combine(oldPath)).Replace("LogisticUploadsTemp", "LogisticUploads");

        string folderPath = HttpContext.Current.Server.MapPath(Path.Combine("~/Uploads/LogisticUploads/" + dateTime.ToString("yyyy/MM/dd").Replace("-", "/")));

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (System.IO.File.Exists(Server.MapPath(oldPath)))
        {
            System.IO.File.Move(oldpath2, newPath2);
        }
    }


    [WebMethod]
    public void UpdateAllRtlCheck(string RTID, bool checkStatus)
    {
        try
        {
            string sql = @"
                            UPDATE TBL_ROUTELINE
						    SET RTL_CHECK =" + Convert.ToInt32(checkStatus) + @"
						    WHERE RTL_RTID = " + RTID;
            con.dbrun(sql);
        }
        catch (Exception ex)
        {

            throw;
        }
    }



    //[WebMethod]
    //public void UpdateRouteLine(string RouteLineData)
    //{
    //    string sql = "";
    //    List <SaveLineModel> rtlList = JsonConvert.DeserializeObject<List<SaveLineModel>>(RouteLineData);

    //    foreach (var rtlitem in rtlList)
    //    {
    //        sql = @"  
    //                UPDATE TBL_ROUTELINE

    //                SET
    //                    RTL_WAGONID = " + rtlitem.WagonId + @",
    //                    RTL_WAGONNO = " + rtlitem.WagonNo + @",
    //                    RTL_CONTAINERID = " + rtlitem.ContainerId + @",
    //                    RTL_CONTAINERNO = " + rtlitem.ContainerNo + @",
    //                    RTL_OVERHEAD = '" + rtlitem.Overhead + @"',
    //                    RTL_UPFID = '" + rtlitem.UpfId + @"',
    //                    RTL_CHECK = '" + rtlitem.Shipname + @"'

    //                    WHERE RT_ID = " + routeModel.ExitDate;

    //        sql_rtl = @"
    //                INSERT INTO TBL_ROUTELINE( RTL_UPFID, RTL_CHECK)
    //                VALUES(
    //                    " + reader["RT_ID"].ToString() + @",
    //                    " + itemRtl.WagonId + @",
    //                    '" + itemRtl.WagonNo + @"',
    //                    " + itemRtl.ContainerId + @",
    //                    '" + itemRtl.ContainerNo + @"',
    //                    '" + itemRtl.Overhead + @"',
    //                    " + itemRtl.UpfId + @",
    //                    '" + itemRtl.TR_Check + @"'

    //                )";

    //        con.dbrun(sql);
    //    }



    //}

    #region ROUTEPAGE
    [WebMethod]
    public void GetRouteData()
    {
        List<GetRouteDataModel> routeList = new List<GetRouteDataModel>();
        string sql = @"
                        SELECT 
                        RT_ID RTID,
                        ORD_FICHENO FICHENO,
                        C_A.name" + lg.Get_Lang() + @" AREA,
                        RT_PORT PORT,
                        C_L.name" + lg.Get_Lang() + @" LOADCOUNTRY,
                        CASE
							WHEN RT_LOADSTID = 0 THEN RT_LOADSTNAME
							ELSE P_L.PNT_CODE + ' - ' + P_L.PNT_NAME
						END LOADSTNAME,
                        C_D.name" + lg.Get_Lang() + @" DESTCOUNTRY,
                        CASE
							WHEN RT_DESTSTID = 0 THEN RT_DESTSTNAME
							ELSE P_D.PNT_CODE + ' - ' + P_D.PNT_NAME
						END DESTSTNAME,
                        RT_SHIPNAME SHIPNAME,
                        SC_VALUE" + lg.Get_Lang() + @" SHIPTYPE,
                        CONVERT(varchar,RT_ENTRYDATE,23) ENTRYDATE,
                        CONVERT(varchar,RT_EXITDATE,23) EXITDATE,
                        RT_STATUS STATUS,
                        CASE
							WHEN RT_FULLEMPTY = 1 THEN 'F '
							ELSE 'E '
						END +   CONCAT(ISNULL(SUM(CountCont.pass),0),' / ',ISNULL(SUM(CountCont.total),0)) CountCont,
						LOWER(C_A.code) FLAG
						
                        FROM TBL_ROUTE
                        LEFT JOIN TBL_TRANSORDERS ON RT_ORDID = ORD_RECNO
                        LEFT JOIN T_COUNTRIES C_A ON RT_AREA = C_A.id
                        LEFT JOIN T_COUNTRIES C_L ON RT_LOADCOUNTRY = C_L.id
                        LEFT JOIN T_COUNTRIES C_D ON RT_DESTCOUNTRY = C_D.id
                        LEFT JOIN TBL_POINTS  P_L ON RT_LOADSTID = P_L.PNT_RECNO
                        LEFT JOIN TBL_POINTS  P_D ON RT_DESTSTID = P_D.PNT_RECNO
                        LEFT JOIN TBL_SPECODES ON SC_REFID = RT_SHIPTYPE AND SC_TYPE = 'SHIPTYPE'
						LEFT JOIN (
						 SELECT RTL_RTID,SUM(RTL_CHECK) pass ,COUNT(*) total--, 
						 FROM TBL_ROUTELINE WHERE RTL_STATUS=0 GROUP BY RTL_CHECK,RTL_RTID --  ORDER BY RTL_RTID 
						) CountCont on CountCont.RTL_RTID=RT_ID
                        WHERE RT_STATUS <> -1 
						group by 
						RT_ID,
						ORD_FICHENO,
						C_A.name" + lg.Get_Lang() + @",
						RT_PORT,
						C_L.name" + lg.Get_Lang() + @",
						P_L.PNT_NAME,
						C_D.name" + lg.Get_Lang() + @",
						P_D.PNT_NAME,
						RT_SHIPNAME,
						SC_VALUE" + lg.Get_Lang() + @",
						RT_ENTRYDATE,
						RT_EXITDATE,
						RT_STATUS,
                        C_A.code,
                        RT_LOADSTID,
						RT_LOADSTNAME,
						RT_DESTSTID,
						RT_DESTSTNAME,
						P_L.PNT_CODE,
						P_D.PNT_CODE,
                        RT_FULLEMPTY";
        reader = con.dbrun(sql);
        while (reader.Read())
        {
            GetRouteDataModel routecls = new GetRouteDataModel();
            routecls.RTID = int.Parse(reader["RTID"].ToString());
            routecls.FICHENO = reader["FICHENO"].ToString();
            routecls.AREA = reader["AREA"].ToString();
            routecls.PORT = reader["PORT"].ToString();
            routecls.LOADCOUNTRY = reader["LOADCOUNTRY"].ToString();
            routecls.LOADSTNAME = reader["LOADSTNAME"].ToString();
            routecls.DESTCOUNTRY = reader["DESTCOUNTRY"].ToString();
            routecls.DESTSTNAME = reader["DESTSTNAME"].ToString();
            routecls.SHIPNAME = reader["SHIPNAME"].ToString();
            routecls.SHIPTYPE = reader["SHIPTYPE"].ToString();
            routecls.ENTRYDATE = reader["ENTRYDATE"].ToString();
            routecls.EXITDATE = reader["EXITDATE"].ToString();
            routecls.EXITDATE = reader["EXITDATE"].ToString();
            routecls.CountCont = reader["CountCont"].ToString();
            routecls.FLAG = reader["FLAG"].ToString();
            routecls.STATUS = reader["STATUS"].ToString();
            routeList.Add(routecls);
        }

        JavaScriptSerializer jss = new JavaScriptSerializer();

        Context.Response.Write(jss.Serialize(routeList));
    }

    [WebMethod]
    public void GetRouteLineData(string RTID)
    {
        List<GetTransportModel> trpList = new List<GetTransportModel>();
        string sql = @"
                            SELECT 
                            0 TRN_ID,
                            RTL_WAGONID WagonId,
                            RTL_WAGONNO WagonNo,
							RTL_CONTAINERID ContainerId,
                            RTL_CONTAINERNO ContainerNo,
                            RTL_OVERHEAD Overhead,
							ISNULL(RTL_ID,0) RtlId,
							ISNULL(RTL_RTID,0) RtId,
							ISNULL(RTL_UPFID,0) UpfId,
							ISNULL(RTL_CHECK,0) TR_Check,
                            UF_FILE UFILE
                            FROM TBL_ROUTELINE
							LEFT JOIN TBL_ROUTE ON RTL_RTID = RT_ID
                            LEFT JOIN TBL_UPFILES ON UF_ID = RTL_UPFID

						    INNER JOIN TBL_TRANSORDERS ON ORD_RECNO = RT_ORDID
							
                            WHERE RTL_STATUS <> -1 AND RTL_RTID=" + RTID;
        reader = con.dbrun(sql);

        while (reader.Read())
        {
            GetTransportModel trpcls = new GetTransportModel();
            trpcls.TRN_ID = int.Parse(reader["TRN_ID"].ToString());
            trpcls.WagonId = reader["WagonId"].ToString();
            trpcls.WagonNo = reader["WagonNo"].ToString();
            trpcls.ContainerId = reader["ContainerId"].ToString();
            trpcls.ContainerNo = reader["ContainerNo"].ToString();
            trpcls.Overhead = reader["Overhead"].ToString();
            trpcls.RtlId = reader["RtlId"].ToString();
            trpcls.RtId = reader["RtId"].ToString();
            trpcls.UpfId = reader["UpfId"].ToString();
            trpcls.TR_Check = reader["TR_Check"].ToString();
            trpcls.File = reader["UFILE"].ToString();


            trpList.Add(trpcls);
        }

        JavaScriptSerializer jss = new JavaScriptSerializer();
        Context.Response.Write(jss.Serialize(trpList));

    }

    [WebMethod]
    public void DeleteRoute(string RTID)
    {
        con.dbrun(@" UPDATE TBL_ROUTE
					 SET RT_STATUS = -1
					 WHERE RT_ID = " + RTID + @";
                     UPDATE TBL_ROUTELINE
                     SET RTL_STATUS = -1
                     WHERE RTL_RTID = " + RTID);
    }
    #endregion



    [WebMethod]
    public void UploadFile()
    {
        string folder = "", folderPath = "";

        string type = HttpContext.Current.Request.Form["type"].ToString();
        if (type == "1")
        {
            folder = "LogisticUploadsTemp";
        }
        else
        {
            folder = "LogisticUploads";
        }
        try
        {
            FileNameModel fnModel = new FileNameModel();
            DateTime dateTime = DateTime.Now;
            if (HttpContext.Current.Request.Files.Count == 0)
            {
                fnModel.fileName = "";
            }
            else
            {
                var httpPostedFile = HttpContext.Current.Request.Files[0];
                FileInfo fileInfo = new FileInfo(httpPostedFile.FileName);
                string uniqueFileName = fileInfo.Name + "_" + dateTime.ToString("yyyy-MM-dd-HH-mm-ss") + fileInfo.Extension;
                folderPath = HttpContext.Current.Server.MapPath(Path.Combine("~/Uploads/" + folder + "/" + dateTime.ToString("yyyy/MM/dd").Replace("-", "/")));

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);


                string serverPath = Path.Combine("Uploads/" + folder + "/" + dateTime.ToString("yyyy/MM/dd").Replace("-", "/"));

                fnModel.fileName = serverPath + "/" + uniqueFileName;

                httpPostedFile.SaveAs(folderPath + "/" + uniqueFileName);

            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Context.Response.Write(jss.Serialize(fnModel));

        }
        catch (Exception ex)
        {

        }

    }


    [WebMethod(EnableSession = true)]
    public void UploadDeletedOrderFile()
    {
        int orderId = Convert.ToInt32(HttpContext.Current.Request.Form["orderId"]);
        string deleteReason = Convert.ToString(HttpContext.Current.Request.Form["deleteReason"]);

        int branch = 0;

        string branchQuery = @"SELECT ORD_B_ID FROM TBL_TRANSORDERS WHERE ORD_RECNO =" + orderId;

        var branchReader = con.dbrun(branchQuery);

        if (branchReader.Read())
            branch = Convert.ToInt32(branchReader["ORD_B_ID"]);

        sys.Set_Status(orderId, 99, branch);

        if (HttpContext.Current.Request.Files.Count > 0)
        {
            FileManager fileManager = new FileManager();
            var postedFile = HttpContext.Current.Request.Files[0];

            string fileQuery = @"SELECT UF_ID FROM TBL_UPFILES WHERE UF_DOCID=" + orderId + " AND UF_APP='DeletedOrderFile'";

            var fileReader = con.dbrun(fileQuery);
            bool insertFile = true;

            if (fileReader.Read()) insertFile = false; //update TBL_UPFILES

            fileManager.UploadFile(type: 6, postedFile: postedFile, docId: orderId, insert: insertFile, fileDesc: deleteReason);
        }
    }

    public class AutoComplateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FileNameModel
    {
        public string fileName { get; set; }
    }

    public class GetTransportModel
    {
        public int TRN_ID { get; set; }
        public string WagonId { get; set; }
        public string WagonNo { get; set; }
        public string ContainerId { get; set; }
        public string ContainerNo { get; set; }
        public string Overhead { get; set; }
        public string ContCatId { get; set; }
        public string ContCat { get; set; }
        public string ContTypeId { get; set; }
        public string ContType { get; set; }
        public string RtlId { get; set; }
        public string RtId { get; set; }
        public string UpfId { get; set; }
        public string TR_Check { get; set; }
        public string File { get; set; }


    }
    public class GetOrderModel
    {
        public string ORDID { get; set; }
        public string OrdFicheno { get; set; }
        public string FicheDate { get; set; }
        public string OrdPodcode { get; set; }
        public string Rejim { get; set; }
        public string Sender { get; set; }
        public string SenderSt { get; set; }
        public string Receiver { get; set; }
        public string ReceiverSt { get; set; }
        public string Client { get; set; }
        public string QnqName { get; set; }
        public string BrdEntry { get; set; }
        public string BrdExit { get; set; }
    }

    public class GetRouteModel
    {
        public int RTID { get; set; }
        public string LoadCountry { get; set; }
        public string DestCountry { get; set; }
        public int LoadStId { get; set; }
        public string LoadStName { get; set; }
        public int DestStId { get; set; }
        public string DestStName { get; set; }
        public int AreaId { get; set; }
        public string Area { get; set; }
        public string Port { get; set; }
        public string Shipname { get; set; }
        public string FullEmpty { get; set; }
        public string Shiptype { get; set; }
        public string EntryDate { get; set; }
        public string ExitDate { get; set; }

    }

    public class GetDataModel
    {
        public List<GetTransportModel> listTrpModel { get; set; }
        public GetOrderModel OrdModel { get; set; }
        public GetRouteModel RouteModel { get; set; }
    }

    public class ComboModel
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }

    public class SaveModel
    {
        public int RTID { get; set; }
        public int ORDID { get; set; }
        public int LoadCountry { get; set; }
        public int DestCountry { get; set; }
        public int LoadStId { get; set; }
        public string LoadStName { get; set; }
        public int DestStId { get; set; }
        public string DestStName { get; set; }
        public int AreaId { get; set; }
        public string Port { get; set; }
        public string Shipname { get; set; }
        public int Shiptype { get; set; }
        public string EntryDate { get; set; }
        public string ExitDate { get; set; }
        public List<SaveLineModel> RouteLine { get; set; }

    }

    public class SaveLineModel
    {
        public int RtlId { get; set; }
        public int RtId { get; set; }
        public int WagonId { get; set; }
        public int ContainerId { get; set; }
        public int UpfId { get; set; }
        public string WagonNo { get; set; }
        public string ContainerNo { get; set; }
        public string Overhead { get; set; }
        public int ContCatId { get; set; }
        public int ContTypeId { get; set; }
        public string TR_Check { get; set; }
        public string File { get; set; }
    }

    public class GetRouteDataModel
    {
        public int RTID { get; set; }
        public int ORDID { get; set; }
        public string FICHENO { get; set; }
        public string AREA { get; set; }
        public string PORT { get; set; }
        public string LOADCOUNTRY { get; set; }
        public string LOADSTID { get; set; }
        public string LOADSTNAME { get; set; }
        public string DESTCOUNTRY { get; set; }
        public string DESTSTID { get; set; }
        public string DESTSTNAME { get; set; }
        public string SHIPNAME { get; set; }
        public string SHIPTYPE { get; set; }
        public string ENTRYDATE { get; set; }
        public string EXITDATE { get; set; }
        public string CountCont { get; set; }
        public string FLAG { get; set; }
        public string STATUS { get; set; }

    }

    public class ReportData
    {
        public List<string> contents { get; set; }
    }

    public class ReportMainData
    {
        public List<string> countries { get; set; }
        public List<ReportData> reportDatas { get; set; }
    }
}
