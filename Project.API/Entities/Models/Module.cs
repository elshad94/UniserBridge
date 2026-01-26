using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class Module
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Value { get; set; }

    public string Color { get; set; }

    public string Icon { get; set; }

    public string Url { get; set; }

    public byte? OrderBy { get; set; }

    public bool? Status { get; set; }
}
