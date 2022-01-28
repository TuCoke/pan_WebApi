using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Exceptions
{
	public class LogicException : UserFriendlyExceptioninBase
	{
		public LogicException(string errorMessage) : base(errorMessage)
		{
		}

		public override int Code { get; set; } = 502;
	}
}
