using Project.Core.Mapper;
using System;
using System.Collections.Generic;

namespace Project.Core.Entities.Models;

public partial class RequestDetail
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string RequestData { get; set; }

    public string ResponseData { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string MethodName { get; set; }
}
