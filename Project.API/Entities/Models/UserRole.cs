using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? RoleId { get; set; }

    public DateTime? Expdate { get; set; }

    public bool? Status { get; set; }
}
