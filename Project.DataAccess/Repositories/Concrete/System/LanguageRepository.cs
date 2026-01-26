using AutoMapper;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.DataAccess.Repositories.Concrete.System
{
    public class LanguageRepository : EntityRepositoryBase<Language, ProjectAppDbContext>, ILanguageRepository
    {
        public LanguageRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
