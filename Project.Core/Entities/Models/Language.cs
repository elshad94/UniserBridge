using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class Language
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public bool? Status { get; set; }
}
