using AutoMapper;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.DataAccess.Repositories.Concrete.System
{
    public class UserRoleRepository : EntityRepositoryBase<UserRole, ProjectAppDbContext>, IUserRoleRepository
    {
        public UserRoleRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}