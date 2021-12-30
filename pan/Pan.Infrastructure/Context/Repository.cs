using Pan.Common;
using Pan.Infrastructure.Base;
using Pan.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Pan.Infrastructure.UnitOfWoks;

namespace Pan.Infrastructure.Context
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly EFCoreDbContext _dbContext;

        public Repository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IUnitOfWork UnitOfWork => _dbContext;

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity)
        {
            if (typeof(TEntity).GetInterface(nameof(IHasCreationTime)) != null)
            {
                var prop = typeof(TEntity).GetProperty(nameof(IHasCreationTime.CreateOn));
                prop.SetValue(entity, DateTime.Now);
            }
            await _dbContext.AddAsync(entity);
        }

        /// <summary>
        /// 添加list 实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (typeof(TEntity).GetInterface(nameof(IHasCreationTime)) != null)
            {
                var prop = typeof(TEntity).GetProperty(nameof(IHasCreationTime.CreateOn));
                var list = entities.ToList();
                list.ForEach(entity => prop.SetValue(entity, DateTime.Now));
                entities = list;
            }
            await _dbContext.AddRangeAsync(entities);
        }

        /// <summary>
        /// IQueryable 查询 (只查询未被删除的)
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQuery()
        {
            var dbset = _dbContext.Set<TEntity>();

            if (typeof(TEntity).GetInterface(nameof(IEntityStatus)) != null)
            {
                // 查询 拼接的 sql = 1
                var expression = new ExpressionBuilder<TEntity>().AndOr(new { Status = EntityStatusEnums.Normal });
                return dbset.Where(expression);
            }
            return dbset.AsQueryable();
        }
        /// <summary>
        /// 执行 sql 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryFromSql(FormattableString sql)
        {
            var dbset = _dbContext.Set<TEntity>().FromSqlInterpolated(sql);
            return dbset.AsQueryable();
        }

        /// <summary>
        /// 查询所有 (包括软删除)
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryWithDelete()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 修改单个实体
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            if (typeof(TEntity).GetInterface(nameof(IHasModiticationTime)) != null)
            {
                var prop = typeof(TEntity).GetProperty(nameof(IHasModiticationTime.UpdateOn));
                prop.SetValue(entity, DateTime.Now);
            }

            _dbContext.Update(entity);

            EntityChangeActionsManager<TEntity>.Current.InvokeFuncs(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (typeof(TEntity).GetInterface(nameof(IHasModiticationTime)) != null)
            {
                var prop = typeof(TEntity).GetProperty(nameof(IHasModiticationTime.UpdateOn));
                var list = entities.ToList();
                list.ForEach(entity => prop.SetValue(entity, DateTime.Now));
                entities = list;
            }

            _dbContext.UpdateRange(entities);

            foreach (var entity in entities)
            {
                EntityChangeActionsManager<TEntity>.Current.InvokeFuncs(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            if (typeof(TEntity).GetInterface(nameof(ISoftDelete)) == null)
            {
                HardDelete(entity);
                return;
            }
            else
            {
                var prop = typeof(TEntity).GetProperty(nameof(ISoftDelete.IsofDelete));
                prop.SetValue(entity, true);
            }

            if (typeof(TEntity).GetInterface(nameof(IHasDeletionTime)) != null)
            {
                var prop = typeof(TEntity).GetProperty(nameof(IHasDeletionTime.DeletionTime));
                prop.SetValue(entity, DateTime.Now);
            }

            _dbContext.Update(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (typeof(TEntity).GetInterface(nameof(ISoftDelete)) == null)
            {
                //直接删除 不再是软删除
                HardDeleteRange(entities);
                return;
            }
            else
            {
                var prop = typeof(TEntity).GetProperty(nameof(ISoftDelete.IsofDelete));
                foreach (var entity in entities)
                {
                    prop.SetValue(entity, true);
                }
            }

            if (typeof(TEntity).GetInterface(nameof(IHasDeletionTime)) != null)
            {
                var prop = typeof(TEntity).GetProperty(nameof(IHasDeletionTime.DeletionTime));
                foreach (var entity in entities)
                {
                    prop.SetValue(entity, DateTime.Now);
                }
            }

            _dbContext.UpdateRange(entities);
        }

        public void HardDelete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public void HardDeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
        }
    }
    public class Repository<TEntity, TPrimaryKey> : Repository<TEntity>, IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntityCore<TPrimaryKey>
    {
        public Repository(EFCoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            var entity = await _dbContext.FindAsync<TEntity>(id);
            if (entity == null)
            {
                Delete(entity);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<TPrimaryKey> ids)
        {
            var entities = await _dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();
            if (entities.Count > 0)
            {
                DeleteRange(entities);
            }
        }

        public async Task HardDeleteAsync(TPrimaryKey id)
        {
            var entity = await _dbContext.FindAsync<TEntity>(id);
            if (entity == null)
            {
                HardDelete(entity);
            }
        }

        public async Task HardDeleteRangeAsync(IEnumerable<TPrimaryKey> ids)
        {
            var entities = await _dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();
            if (entities.Count > 0)
            {
                HardDeleteRange(entities);
            }
        }
    }
    public static class RepositoryExtension
    {
        public static IQueryable<TEntity> GetQueryWithDisable<TEntity>(this IRepository<TEntity> repository) where TEntity : class, IEntityStatus
        {
            var query = repository.GetQueryWithDelete();
            return query.Where(x => x.Status != EntityStatusEnums.Delete);
        }

        public static async Task DeleteAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TPrimaryKey Id) where TEntity : BaseEntityCore<TPrimaryKey>, IEntityStatus
        {
            var entity = await repository.GetAsync(Id);
            if (entity == null) return;

            entity.Status = EntityStatusEnums.Delete;
            repository.Update(entity);
        }
    }
}
