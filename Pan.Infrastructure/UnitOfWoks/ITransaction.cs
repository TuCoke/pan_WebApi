using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Pan.Infrastructure.UnitOfWoks
{
	public interface ITransaction
	{
		bool HasActiveTransaction { get; }

		IDbContextTransaction GetCurrent();

		Task RollbackAsync();

		Task<IDbContextTransaction> BeginAsync();

		Task CommitAsync(IDbContextTransaction transaction);
	}
}
