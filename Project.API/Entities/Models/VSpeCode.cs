using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class VSpeCode
{
    public string Lang { get; set; }

    public int? RefId { get; set; }

    public string Value { get; set; }

    public string KeyType { get; set; }

    public short? OrderBy { get; set; }
}
