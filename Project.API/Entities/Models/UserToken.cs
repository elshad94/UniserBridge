using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class UserToken
{
    public int Id { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    /// <summary>
    /// AccessToken endate
    /// </summary>
    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }

    public bool LogOut { get; set; }
}
