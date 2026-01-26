using DocumentFormat.OpenXml.Math;
using Project.Core.DataAccess.Abstract;
using Project.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Repositories.Abstract.RequestOperations
{
    public interface IRequestRepository : IEntityRepositoryBase<RequestDetail>
    {
    }
}
