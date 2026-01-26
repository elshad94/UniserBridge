using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class SpeCode
{
    public int Id { get; set; }

    public string Type { get; set; }

    public int? RefId { get; set; }

    public string Code { get; set; }

    public string Value { get; set; }

    public short? OrderBy { get; set; }

    public bool? Status { get; set; }
}
