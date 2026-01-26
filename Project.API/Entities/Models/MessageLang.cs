using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class MessageLang
{
    public int Id { get; set; }

    public int? MessageId { get; set; }

    public int? LangId { get; set; }

    public string Value { get; set; }

    public bool? Status { get; set; }
}
