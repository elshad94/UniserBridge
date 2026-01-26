using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class SystemLog
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Content { get; set; }

    public string RequestUrl { get; set; }

    public int? UserId { get; set; }

    public DateTime CreateDate { get; set; }
}
