using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class City
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public string Name { get; set; }

    public bool? Status { get; set; }
}
