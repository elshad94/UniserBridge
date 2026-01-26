using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class CardLang
{
    public int Id { get; set; }

    public string Type { get; set; }

    public int? LangId { get; set; }

    public int? CardId { get; set; }

    public string Value { get; set; }

    public bool? Status { get; set; }
}
