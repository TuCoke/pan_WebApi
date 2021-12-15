﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pan.Infrastructure.UnitOfWoks
{
	public interface IUnitOfWork : IDisposable
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
	}
}
