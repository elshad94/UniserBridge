using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class RoleMenu
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public int? MenuId { get; set; }

    public bool? Status { get; set; }
}
