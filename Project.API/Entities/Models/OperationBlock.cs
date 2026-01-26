using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class OperationBlock
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public DateTime? Date { get; set; }

    public bool? Status { get; set; }
}
