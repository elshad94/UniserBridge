using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class UserLoginHistory
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? LoginDate { get; set; }
}
