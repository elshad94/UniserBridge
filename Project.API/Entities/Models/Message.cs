using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class Message
{
    public int Id { get; set; }

    public int? Code { get; set; }

    public string Definition { get; set; }

    public string Note { get; set; }

    public bool? Status { get; set; }
}
