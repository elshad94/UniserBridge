using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class FileUpload
{
    public int Id { get; set; }

    public Guid DownloadKey { get; set; }

    /// <summary>
    /// Table Name
    /// </summary>
    public string TableName { get; set; }

    public int? TableId { get; set; }

    public short? DocType { get; set; }

    public string FileName { get; set; }

    public string Url { get; set; }

    public int? CreateUserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? ExistsOnTheServer { get; set; }

    public bool? Status { get; set; }
}
