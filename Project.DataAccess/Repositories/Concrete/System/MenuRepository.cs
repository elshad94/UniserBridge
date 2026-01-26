using AutoMapper;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.DataAccess.Repositories.Concrete.System
{
    public class MenuRepository : EntityRepositoryBase<Menu, ProjectAppDbContext>, IMenuRepository
    {
        public MenuRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}