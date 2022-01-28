using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Base
{
	public interface IHasModiticationTime
	{
		public DateTime? UpdateOn { get; set; }
	}

	public interface IHasModification : IHasModiticationTime
	{
		public string UpdateUser { get; set; }
	}
}
