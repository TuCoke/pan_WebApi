using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Common.Encryptions
{
	public static class MD5Encryption
	{
		public static string ComputeHash(string inputValue)
		{
			var bytes = Encoding.UTF8.GetBytes(inputValue);
			return ComputeHash(bytes);
		}

		public static string ComputeHash(byte[] bytes)
		{
			using var md5 = MD5.Create();
			var result = md5.ComputeHash(bytes);
			var strResult = BitConverter.ToString(result);
			return strResult.Replace("-", "").ToLower();
		}
	}
}
