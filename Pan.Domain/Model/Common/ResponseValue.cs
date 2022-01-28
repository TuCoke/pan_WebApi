﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pan.Domain.Model.Common
{
	public class ResponseValue : ResponseValue<string>
	{
		
		public ResponseValue()
		{
			Code = 200;
			IsSuccess = true;
		}
		/// <summary>
		/// 错误
		/// </summary>
		/// <param name="errorMsg"></param>
		public ResponseValue(string errorMsg)
		{
			Code = 500;
			IsSuccess = false;
			ErrorMsg = errorMsg;
		}

	}

	public class ResponseValue<T>
	{
		public ResponseValue()
		{
			Code = 200;
			IsSuccess = true;
		}

		public ResponseValue(T data)
		{
			Code = 200;
			IsSuccess = true;
			Data = data;
		}

		public ResponseValue(string errorMsg)
		{
			Code = 500;
			IsSuccess = false;
			ErrorMsg = errorMsg;
		}

		public bool IsSuccess { get; set; }
		public string ErrorMsg { get; set; }
		public int Code { get; set; }
		public T Data { get; set; }
	}
}
