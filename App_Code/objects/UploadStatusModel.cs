using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UploadStatusModel
/// </summary>
public class UploadStatusModel
{
    public bool IsSucceed { get; set; }
    public string UploadedFileInfo { get; set; }
    public string Message { get; set; }

    public UploadStatusModel()
    {
        IsSucceed = true;
        UploadedFileInfo = "";
        Message = "";
    }
}