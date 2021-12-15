using Pan.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Context
{
    public interface IRepository<TEntity>
    {
		IQueryable<TEntity> GetQueryWithDelete();

		//IQueryable<TEntity> GetQueryWithDisable();

		IQueryable<TEntity> GetQuery();

		IQueryable<TEntity> GetQueryFromSql(FormattableString sql);

		Task AddAsync(TEntity entity);

		Task AddRangeAsync(IEnumerable<TEntity> entities);

		void Update(TEntity entity);

		void UpdateRange(IEnumerable<TEntity> entities);

		//void Delete(TEntity entity);

		// void DeleteRange(IEnumerable<TEntity> entities);

		//void HardDelete(TEntity entity);

		//void HardDeleteRange(IEnumerable<TEntity> entities);

		Task SaveChangesAsync();
	}

	public interface IRepository<TEntity, TPrimaryKey> : IRepository<TEntity> where TEntity : BaseEntityCore<TPrimaryKey>
	{
		Task<TEntity> GetAsync(TPrimaryKey id);

		//Task DeleteAsync(TPrimaryKey id);

		//Task DeleteRangeAsync(IEnumerable<TPrimaryKey> ids);

		//Task HardDeleteAsync(TPrimaryKey id);

		//Task HardDeleteRangeAsync(IEnumerable<TPrimaryKey> ids);
	}
}
