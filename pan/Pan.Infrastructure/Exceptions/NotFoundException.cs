using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Exceptions
{
	public class NotFoundException : UserFriendlyExceptioninBase
	{
		public NotFoundException()
		{
			ErrorMessage = "未找到相对应的数据实体！";
		}

		public NotFoundException(string errorMessage) : base(errorMessage)
		{
		}

		public override int Code { get; set; } = 404;
	}
}
