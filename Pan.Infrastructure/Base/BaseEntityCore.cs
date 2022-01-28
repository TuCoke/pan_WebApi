using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Base
{
	//public abstract class BaseEntityCore : BaseEntityCore<Guid>
	//{
	//}

	//public abstract class BaseEntityCore<TPrimaryKey> : IEntity
	//{

	//	/// <summary>
	//	/// 主键
	//	/// </summary>
	//	[Key]
	//	public TPrimaryKey Id { get; set; }
	//}
	public abstract class BaseEntityCore : BaseEntityCore<int>
	{
	}

	public abstract class BaseEntityCore<TPrimaryKey> : IEntity
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public TPrimaryKey Id { get; set; }
	}
}
