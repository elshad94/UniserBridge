using AutoMapper;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.DataAccess.Repositories.Concrete.System
{
    public class ModuleRepository : EntityRepositoryBase<Module, ProjectAppDbContext>, IModuleRepository
    {
        public ModuleRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}