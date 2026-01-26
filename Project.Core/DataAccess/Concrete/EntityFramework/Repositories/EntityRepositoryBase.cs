using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Project.Core.DataAccess.Abstract;
using Project.Core.Entities.Abstract;
using Project.Core.Utilities.Results;
using Microsoft.EntityFrameworkCore.Query;

namespace Project.Core.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
         where TEntity : class, new()
         where TContext : DbContext, new()
    {
        protected IMapper Mapper;

        public EntityRepositoryBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        //public TEntity Get(Expression<Func<TEntity, bool>> filter)
        //{
        //    using var context = new TContext();

        //    return context.Set<TEntity>().FirstOrDefault(filter);
        //}

        public TEntity Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            using var context = new TContext();

            IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
            if (include != null) queryable = include(queryable);

            var run = queryable.FirstOrDefault(filter);
            return run;
        }



        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();

            return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
        }

        public OperationResult Add(TEntity entity, int? parentAuditId = null) 
            => ExecuteOperation(entity, EntityState.Added, parentAuditId);

        public OperationResult Update(TEntity entity, 
            int? parentAuditId = null) 
            => ExecuteOperation(entity, EntityState.Modified, parentAuditId);

        public TEntity FindById(int id)
        {
            using var context = new TContext();

            return context.Set<TEntity>().Find(id);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();

            return context.Set<TEntity>().Any(filter);
        }

        private OperationResult ExecuteOperation(TEntity entity, EntityState state, int? parentAuditId)
        {
            //if (IsBlockedOperation(entity))
            //{
            //    return new OperationResult
            //    {
            //        ResultInfo = ResultInfo.BlockedOperation
            //    };
            //}

            var entityType = typeof(TEntity);
            var typeName = entityType.Name;
            var auditDtoType = Assembly
                    .GetExecutingAssembly()
                    .GetExportedTypes()
                    .FirstOrDefault(type => type.Name.Equals($"{typeName}AuditDto"));
            var affectedRows = 0;

            var result = new OperationResult();

            if (auditDtoType != null)
            {
                IAuditDto auditDtoObject;

                switch (state)
                {
                    case EntityState.Added:
                        affectedRows = SaveChanges(entity, state);
                        if (affectedRows == 0)
                        {
                            break;
                        }

                        auditDtoObject = (IAuditDto)Mapper.Map(entity, entityType, auditDtoType);
                        InitAuditDto(auditDtoType, auditDtoObject,
                            "Ins", parentAuditId);

                        SaveChanges(auditDtoObject, EntityState.Added);
                        result.AuditId = auditDtoObject.AuditId;
                        break;
                    case EntityState.Modified:
                        using (var context = new TContext())
                        {
                            var unmodifiedEntity = context.Find(entityType, ((dynamic)entity).Id);
                            auditDtoObject = (IAuditDto)Mapper.Map(unmodifiedEntity, entityType, auditDtoType);
                            InitAuditDto(auditDtoType, auditDtoObject,
                                "Upd", parentAuditId);
                        }

                        SaveChanges(auditDtoObject, EntityState.Added);
                        result.AuditId = auditDtoObject.AuditId;

                        affectedRows = SaveChanges(entity, state);
                        break;
                }
            }

            else
            {
                affectedRows = SaveChanges(entity, state);
            }

            result.ResultInfo = affectedRows > 0 ? ResultInfo.SaveSuccess : ResultInfo.SaveFailure;

            return result;
        }

        //private static bool IsBlockedOperation(TEntity entity)
        //{
        //    var url = CurrentScopeDataContainer.Instance.RequestUrl;

        //    const string searchValue = "/api/";
        //    var startIndex = url.IndexOf(searchValue) + searchValue.Length;
        //    var lastIndex = url.IndexOf('/', startIndex);
        //    var moduleName = url.Substring(startIndex, lastIndex - startIndex);

        //    using var context = new TContext();
        //    var blockInfo = context
        //        .Set<OperationBlock>()
        //        .FirstOrDefault(o =>
        //            o.Type.ToLower() == moduleName.ToLower() && o.Status == true);
        //    DateTime entityDate;

        //    try
        //    {
        //        entityDate = ((dynamic)entity).Date;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    return blockInfo != null && blockInfo.Date >= entityDate;
        //}

        private static void InitAuditDto(
            Type auditDtoType,
            IAuditDto auditDto, 
            string oprType,
            int? parentAuditId)
        {
            auditDto.LoprType = oprType;
            auditDto.LuserId = CurrentScopeDataContainer.Instance.UserId;
            auditDto.Ldate = DateTime.Now;

            var parentAuditIdProp = auditDtoType
                .GetProperties()
                .FirstOrDefault(p => 
                    string.Equals(p.Name, "LParentAuditId",
                        StringComparison.CurrentCultureIgnoreCase));

            if (parentAuditIdProp == null)
            {
                return;
            }

            if (parentAuditId == null)
            {
                throw new ArgumentException(
                    $"[{nameof(parentAuditId)}] cannot be null. Audit type name: [{auditDtoType.Name}]");
            }
            
            parentAuditIdProp.SetValue(auditDto, parentAuditId.Value);
        }

        private static int SaveChanges(object entity, EntityState state)
        {
            using var context = new TContext();
            var entry = context.Entry(entity);
            entry.State = state;

            return context.SaveChanges();
        }
    }
}
