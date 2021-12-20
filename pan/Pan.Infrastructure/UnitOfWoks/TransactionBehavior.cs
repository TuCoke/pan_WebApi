using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pan.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pan.Infrastructure.UnitOfWoks
{
	public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TDbContext : EFCoreDbContext
	{
		private readonly ILogger<TransactionBehavior<TDbContext, TRequest, TResponse>> _logger;
		private readonly TDbContext _dbContext;
		//private readonly ICapPublisher _capPublisher;
		public TransactionBehavior(TDbContext dbContext, ILogger<TransactionBehavior<TDbContext, TRequest, TResponse>> logger/*, ICapPublisher capPublisher*/)
		{
			_logger = logger;
			_dbContext = dbContext;
			//_capPublisher = capPublisher;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			//if (request.GetType().GetCustomAttributes(typeof(TransactionAttribute), true).Length == 0)
			//{
			//	return await next();
			//}

			var response = default(TResponse);
			var typeName = string.Empty;

			try
			{
				if (_dbContext.HasActiveTransaction)
				{
					return await next();
				}

				var strategy = _dbContext.Database.CreateExecutionStrategy();

				await strategy.ExecuteAsync(async () =>
				{
					Guid transactionId;
					using (var transaction = await _dbContext.BeginAsync())
					using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
					{
						_logger.LogInformation("----- 开始事务 {TransactionId} ({@Command})", transaction.TransactionId, typeName, request);

						response = await next();

						_logger.LogInformation("----- 提交事务 {TransactionId} {CommandName}", transaction.TransactionId, typeName);


						await _dbContext.CommitAsync(transaction);

						transactionId = transaction.TransactionId;
					}
				});

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "处理事务出错 {CommandName} ({@Command})", typeName, request);
				//return response;
				throw;
			}
		}
	}

	public class TransactionBehavior<TRequest, TResponse> : TransactionBehavior<EFCoreDbContext, TRequest, TResponse>
	{
		public TransactionBehavior(EFCoreDbContext dbContext, ILogger<TransactionBehavior<TRequest, TResponse>> logger/*, ICapPublisher capPublisher*/) : base(dbContext, logger/*, capPublisher*/)
		{
		}
	}
}
