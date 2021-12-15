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
        /// IQueryable 查询
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
        /// sql 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryFromSql(FormattableString sql)
        {
            var dbset = _dbContext.Set<TEntity>().FromSqlInterpolated(sql);
            return dbset.AsQueryable();
        }

        public IQueryable<TEntity> GetQueryWithDelete()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
