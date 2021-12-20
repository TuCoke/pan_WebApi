using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Entity
{
	public class Admin : BaseEntity
	{
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 登录账号
		/// </summary>
		public string Account { get; set; }

		/// <summary>
		/// 手机号
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// 邮箱
		/// </summary>
		public string Mail { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		public GenderEnums? Gender { get; set; }

		/// <summary>
		/// 加密串
		/// </summary>
		public string Salt { get; set; }

		/// <summary>
		/// 密码加密串
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// 最后登录时间
		/// </summary>
		public DateTime LastLoginTime { get; set; }
	}
}
