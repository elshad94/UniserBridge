using DevExpress.Web.ASPxUploadControl;
using System;
using System.Web;
using System.IO;
using DevExpress.XtraReports.UI;

public class FileManager
{
    login lg = new login();

    public UploadStatusModel UploadFile(int type, UploadedFile uploadedFile=null, XtraReport generatedReportFile =null, HttpPostedFile postedFile = null, int docId = 0, string fileDesc="", bool uploadFileNameCheckup=false, bool insert=true)
    {
        string sessionId = HttpContext.Current.Session.SessionID;
        string query = "", uniqueFileName = "", fileLength = "", mainFolder = "", prefix = "", fileName = "";
        DateTime dateTime = DateTime.Now;
        UploadStatusModel uploadStatusModel = new UploadStatusModel();
      
        switch (type)
        {
            case 1:
                mainFolder = "Reports";
                prefix = "INV";
                break;
            case 2:
                mainFolder = "Contracts";
                prefix = "CT";
                break;
            case 3:
                mainFolder = "Stations";
                prefix = "FACT";
                break;

            case 4:
                mainFolder = "Online";
                prefix = "Online";
                break;
            case 5:
                mainFolder = "Reports";
                prefix = "ORD";
                break;
            case 6:
                mainFolder = "DeletedOrderFiles";
                prefix = "DeletedOrderFile";
                break;
        }

        string folderPath = HttpContext.Current.Server.MapPath(Path.Combine("~/Uploads/" + mainFolder + "/" + dateTime.ToString("yyyy/MM/dd").Replace('-','\\')));

        try
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            if(generatedReportFile != null)
            {
                fileName = "inv.pdf";
                uniqueFileName = prefix + "001_" + dateTime.ToString("yyyy/MM/dd/HH/mm/ss/fff").Replace("/", "") + fileName;
                generatedReportFile.ExportToPdf(folderPath + "/" + uniqueFileName);
            }
            else
            {
                if(uploadedFile != null && uploadFileNameCheckup == true)
                {
                    fileName = uploadedFile.FileName;
                    fileLength = uploadedFile.ContentLength / 1024 + "Kb";
                    uniqueFileName = fileName;
                    uploadedFile.SaveAs(folderPath + "/" + uniqueFileName);
                }
                else if(uploadedFile != null)
                {
                    string extension = Path.GetExtension(uploadedFile.FileName);
                    fileName = uploadedFile.FileName; 
                    fileLength = uploadedFile.ContentLength / 1024 + "Kb";
                    uniqueFileName = prefix + "001_" + dateTime.ToString("yyyy/MM/dd/HH/mm/ss/fff").Replace("/", "").Replace(" ", "") + extension;
                    uploadedFile.SaveAs(folderPath + "/" + uniqueFileName);
                }
                else
                {
                    string extension = Path.GetExtension(postedFile.FileName);
                    fileName = postedFile.FileName;
                    fileLength = postedFile.ContentLength / 1024 + "Kb";
                    uniqueFileName = prefix + "001_" + dateTime.ToString("yyyy/MM/dd/HH/mm/ss/fff").Replace("/", "").Replace(" ", "") + extension;
                    postedFile.SaveAs(folderPath + "/" + uniqueFileName);
                }

            }
        }
        catch (Exception ex)
        {
            uploadStatusModel.IsSucceed = false;
            if(uploadedFile!=null)
                uploadStatusModel.Message = "Seçilmiş fayl düzgün formatda deyil.";
            
            return uploadStatusModel;
        }


        string databasePath = "\\Uploads\\" + mainFolder + "\\" + dateTime.ToString("yyyy/MM/dd").Replace('-', '\\') + "\\" + uniqueFileName;
        
        if (uploadedFile != null)
            uploadStatusModel.UploadedFileInfo = string.Format("{0} ({1})|{2}", fileName, fileLength, databasePath);
        else
            uploadStatusModel.UploadedFileInfo = uniqueFileName;

        if (uploadStatusModel.IsSucceed)
        {
            if (insert)
            {
                if (docId == 0)
                {
                    query = @"INSERT INTO TBL_UPFILES (UF_SESSID,UF_TYPE,UF_APP,UF_FILE,UF_DOCID,UF_UFILE,UF_U_ID,UF_TYPE2, UF_DESC) VALUES(N'" + sessionId + "',1,'" + prefix + "',N'" + databasePath + "'," + docId + ",N'" + uniqueFileName + "'," + lg.Get_userid() + "," + 1 + ", N'" + fileDesc + "')";
                }
                else
                {
                    if (uploadedFile == null && postedFile == null)
                        prefix = "ORD";

                    query = "INSERT INTO TBL_UPFILES (UF_SESSID,UF_STATUS,UF_TYPE,UF_APP,UF_FILE,UF_DOCID,UF_UFILE,UF_U_ID,UF_TYPE2, UF_DESC) VALUES(N'" + sessionId + "', 1, 1,'" + prefix + "',N'" + databasePath + "'," + docId + ",N'" + uniqueFileName + "'," + lg.Get_userid() + "," + 1 + ", N'" + fileDesc + "')";
                }
            }
            else
            {
                query = "UPDATE TBL_UPFILES SET UF_SESSID=N'" + sessionId + "', UF_APP='" + prefix + "', UF_FILE=N'" + databasePath + "', UF_UFILE=N'" + uniqueFileName + "', UF_U_ID=" + lg.Get_userid() + ", UF_DESC=N'" + fileDesc + "' WHERE UF_APP='DeletedOrderFile' AND UF_DOCID=" + docId;  
            }

            conn.fnc_dbexecute(query).ToString();
        }

        return uploadStatusModel;
    }
}