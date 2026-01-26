using System;
using System.Collections.Generic;

namespace Project.API.Entities.Models;

public partial class Menu
{
    public int Id { get; set; }

    public int? ModulId { get; set; }

    public int? ParentId { get; set; }

    /// <summary>
    /// 0-esas menu, 1-alt, 2-1in alt menusu
    /// </summary>
    public short? MenuType { get; set; }

    public string Defination { get; set; }

    public string Icon { get; set; }

    public string Link { get; set; }

    public short? Orderby { get; set; }

    public bool? Status { get; set; }
}
