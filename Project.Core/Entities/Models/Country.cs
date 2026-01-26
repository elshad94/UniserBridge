using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Region { get; set; }

    public bool? Status { get; set; }
}
