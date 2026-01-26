using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class MenuInfo
{
    public int Id { get; set; }

    public int? MenuId { get; set; }

    public string PageName { get; set; }

    /// <summary>
    /// QueryString ile gelen parametr adi
    /// </summary>
    public string KeyfieldName { get; set; }

    public byte[] Description { get; set; }

    public bool? Status { get; set; }
}
