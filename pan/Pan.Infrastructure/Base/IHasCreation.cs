using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Base
{
	public interface IHasCreationTime
	{
		public DateTime CreateOn { get; set; }
	}

	public interface IHasCreation : IHasCreationTime
	{
		public string CreatorUser { get; set; }
	}
}
