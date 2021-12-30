using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Domain.Model.Common
{
	public class PagedResultDto<T>
	{
		public PagedResultDto(IEnumerable<T> items, long totalCount)
		{
			TotalCount = totalCount;
			Items = items;
		}

		public long TotalCount { get; private set; }

		public IEnumerable<T> Items { get; private set; }
	}
}
