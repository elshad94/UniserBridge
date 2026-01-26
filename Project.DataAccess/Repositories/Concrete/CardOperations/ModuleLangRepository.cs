using AutoMapper;
using Project.DataAccess.Repositories.Abstract.CardOperations;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;

namespace Project.DataAccess.Repositories.Concrete.CardOperations
{
    public class ModuleLangRepository : EntityRepositoryBase<ModuleLang, ProjectAppDbContext>, IModuleLangRepository
    {
        private readonly IMapper _mapper;
        public ModuleLangRepository(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;

        }
    }
}
