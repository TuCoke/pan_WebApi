using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Exceptions
{
	public class UserFriendlyExceptioninBase : Exception
	{
		public UserFriendlyExceptioninBase()
		{
		}

		public UserFriendlyExceptioninBase(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public string ErrorMessage { get; set; }

		public virtual int Code { get; set; } = 200;
	}
}
