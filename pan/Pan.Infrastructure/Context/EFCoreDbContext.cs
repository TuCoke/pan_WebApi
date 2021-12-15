using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pan.Infrastructure.Base;
using Pan.Infrastructure.UnitOfWoks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Context
{
    public class EFCoreDbContext : DbContext, IUnitOfWork, ITransaction
    {
		protected readonly IMediator _mediator;
		//private readonly ICapPublisher _capPublisher;

		public IDbContextTransaction _currentTransaction;
		protected Assembly Assembly = typeof(EFCoreDbContext).Assembly;

		public EFCoreDbContext(DbContextOptions options, IMediator mediator/*, ICapPublisher capPublisher*/) : base(options)
		{
			_mediator = mediator;
			//_capPublisher = capPublisher;
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var entities = Assembly.ExportedTypes
				.Where
				(t => t.IsClass && !t.IsAbstract && typeof(IEntity).IsAssignableFrom(t));
			foreach (Type type in entities)
			{
				var method = modelBuilder.GetType().GetMethods()
					.FirstOrDefault(c => c.IsGenericMethod && c.Name == "Entity");

				method = method.MakeGenericMethod(new Type[] { type });
				method.Invoke(modelBuilder, null);
			}

			base.OnModelCreating(modelBuilder);
			// 配置数据库
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly);
		}


		public bool HasActiveTransaction => _currentTransaction != null;

		public IDbContextTransaction GetCurrent() => _currentTransaction;

		public Task<IDbContextTransaction> BeginAsync()
		{
			if (_currentTransaction != null) return null;
			_currentTransaction = Database.BeginTransaction();
			return Task.FromResult(_currentTransaction);
		}

		public async Task CommitAsync(IDbContextTransaction transaction)
		{
			if (transaction == null)
				throw new ArgumentNullException(nameof(transaction));

			if (transaction != _currentTransaction)
				throw new InvalidOperationException($"Transaction {transaction} is not current.");

			try
			{
				await SaveChangesAsync();
				transaction.Commit();
			}
			catch
			{
				await RollbackAsync();
				throw;
			}
			finally
			{
				if (_currentTransaction != null)
				{
					await _currentTransaction.DisposeAsync();
					_currentTransaction = null;
				}
			}
		}

		public async Task RollbackAsync()
		{
			try
			{
				await _currentTransaction?.RollbackAsync();
			}
			finally
			{
				if (_currentTransaction != null)
				{
					await _currentTransaction.DisposeAsync();
					_currentTransaction = null;
				}
			}
		}

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
