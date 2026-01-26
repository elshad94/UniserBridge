using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class CardLangAudit
{
    public int AuditId { get; set; }

    public string Type { get; set; }

    public int? LangId { get; set; }

    public int? CardId { get; set; }

    public string Value { get; set; }

    public int? Id { get; set; }

    public string LoprType { get; set; }

    public int? LuserId { get; set; }

    public DateTime? Ldate { get; set; }
}
