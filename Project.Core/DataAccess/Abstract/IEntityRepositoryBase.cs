using System.Linq.Expressions;
using Project.Core.Utilities.Results;
using Microsoft.EntityFrameworkCore.Query;

namespace Project.Core.DataAccess.Abstract
{
    public interface IEntityRepositoryBase<TEntity>
    {
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

        TEntity Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        TEntity FindById(int id);

        bool Any(Expression<Func<TEntity, bool>> filter);

        OperationResult Add(TEntity entity, int? parentAuditId = null);

        OperationResult Update(TEntity entity, int? parentAuditId = null);
    }
}