using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class ObjectContent
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public string PageName { get; set; }

    public byte? OrderBy { get; set; }

    public bool? Status { get; set; }
}
