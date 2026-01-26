using AutoMapper;
using Project.DataAccess.Repositories.Abstract.CardOperations;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.Core.Entities.Models;

namespace Project.DataAccess.Repositories.Concrete.CardOperations
{
    public class ObjectContentRepository : EntityRepositoryBase<ObjectContentsLang, ProjectAppDbContext>, IObjectContentsRepository
    {
        public ObjectContentRepository(IMapper mapper) : base(mapper)
        {
        }
    }
}
