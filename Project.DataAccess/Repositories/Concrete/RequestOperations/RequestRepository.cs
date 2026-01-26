using AutoMapper;
using DocumentFormat.OpenXml.Math;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;
using Project.DataAccess.Repositories.Abstract.RequestOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Repositories.Concrete.RequestOperations
{
    public class RequestRepository : EntityRepositoryBase<RequestDetail, ProjectAppDbContext>, IRequestRepository
    {
        private readonly IMapper _mapper;

        public RequestRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
