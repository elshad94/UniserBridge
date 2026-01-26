using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class FileUploadSetting
{
    public int Id { get; set; }

    public string Extension { get; set; }

    public string ContentType { get; set; }

    public int SizeInMegabyte { get; set; }

    public DateTime CreateDate { get; set; }

    public bool? Status { get; set; }
}
