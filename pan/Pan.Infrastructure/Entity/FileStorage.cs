using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Infrastructure.Entity
{
	public class FileStorage : BaseEntity
	{
		/// <summary>
		/// 文件后缀
		/// </summary>
		public string FileExt { get; set; }

		/// <summary>
		/// 文件大小
		/// </summary>
		public double FileSize { get; set; }

		/// <summary>
		/// 文件类型
		/// </summary>
		public int FileType { get; set; }

		/// <summary>
		/// hash值
		/// </summary>
		public string HashCode { get; set; }

		/// <summary>
		/// 部分目录
		/// </summary>
		public string PartDir { get; set; }

		/// <summary>
		/// 本地路径
		/// </summary>
		public string PathLocal { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// oss文件名
		/// </summary>
		public string OSSName { get; set; }

		/// <summary>
		/// oss桶名称
		/// </summary>
		public string OSSBucketName { get; set; }

		/// <summary>
		/// ETag用于标识一个Object的内容，ETag值验证数据完整性
		/// </summary>
		public string OSSETag { get; set; }

		/// <summary>
		/// oss对象请求id
		/// </summary>
		public string OSSRequestId { get; set; }
	}
}
