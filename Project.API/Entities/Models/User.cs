using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime? Birthday { get; set; }

    public string FinCode { get; set; }

    public bool? Gender { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool PasswordStatus { get; set; }

    public string Email { get; set; }

    public string Phone1 { get; set; }

    public string Phone2 { get; set; }

    public bool? Status { get; set; }

    public DateTime CreateDate { get; set; }
}
