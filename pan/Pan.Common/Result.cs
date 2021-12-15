using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Pan.Common
{
	/// <summary> 
	/// 作者：zhangw 
	/// 时间：2017/4/8 17:34:56
	/// CLR版本：4.0.30319.42000
	/// 唯一标识：2c59861f-3141-477c-9481-8e4555ee7d32 
	/// </summary> 
	public interface IResult : IResult<object> { }

	/// <summary> 
	/// 作者：zhangw 
	/// 时间：2017/4/8 17:34:56
	/// CLR版本：4.0.30319.42000
	/// 唯一标识：2c59861f-3141-477c-9481-8e4555ee7d32 
	/// </summary> 
	public interface IResult<T>
	{
		bool Success { get; }
		string Message { get; }
		T Body { get; }
		string Code { get; }

		IResult<T> Succeed();
		IResult<T> Succeed(T body);
		IResult<T> SetBody(T body);
		IResult<T> SetCode(string code);
		IResult<T> SetMessage(string message);
		IResult<T> Fail();
		IResult<T> Fail(string message);
		IResult<T> Fail(Exception exception);
		IResult<T> Judge(bool status);
		/// <summary>
		/// lowcase key
		/// </summary>
		/// <returns></returns>
		object ToObject();
		void Clear();
		/// <summary>
		/// to json
		/// </summary>
		/// <returns></returns>
		string ToString();
	}

	[Serializable]
	public class Result : Result<object>, IResult
	{
		private Result() { }
		new public static IResult Create()
		{
			return new Result();
		}
	}

	[Serializable]
	public class Result<T> : IResult<T>
	{
		protected Result() { }

		public bool Success { get; private set; }
		/// <summary>
		/// commonly used type
		/// HttpStatusCode.OK / HttpStatusCode.InternalServerError
		/// </summary>
		public string Code { get; private set; }
		public string Message { get; private set; }
		public T Body { get; private set; }

		/// <summary>
		/// 转json字符串
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return JsonConvert.SerializeObject(ToObject());
		}

		/// <summary>
		/// 将键转小写
		/// </summary>
		/// <returns></returns>
		public virtual object ToObject()
		{
			return new
			{
				success = Success,
				code = Code,
				message = Message,
				body = Body
			};
		}

		public static IResult<T> Create()
		{
			return new Result<T>();
		}
		public static IResult<T> Create(T body)
		{
			return new Result<T>().Succeed(body);
		}

		public static IResult<U> Create<U>()
		{
			return new Result<U>();
		}
		public static IResult<U> Create<U>(U body)
		{
			return new Result<U>().Succeed(body);
		}

		/// <summary>
		/// 操作成功
		/// </summary>
		/// <returns></returns>
		public virtual IResult<T> Succeed()
		{
			Success = true;
			Code = ((int)HttpStatusCode.OK).ToString();
			return this;
		}
		/// <summary>
		/// 操作成功
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public virtual IResult<T> Succeed(T body)
		{
			return Succeed().SetBody(body);
		}
		public virtual IResult<T> SetBody(T body)
		{
			Body = body;
			return this;
		}
		public virtual IResult<T> SetMessage(string message)
		{
			Message = message;
			return this;
		}
		public virtual IResult<T> SetCode(string code)
		{
			Code = code;
			return this;
		}
		public virtual IResult<T> Fail()
		{
			Success = false;
			Code = ((int)HttpStatusCode.InternalServerError).ToString();
			return this;
		}
		public virtual IResult<T> Fail(string message)
		{
			return Fail().SetMessage(message);
		}
		public virtual IResult<T> Fail(Exception exception)
		{
			string _message = exception.Message;
			if (string.IsNullOrWhiteSpace(_message) && exception.InnerException != null)
			{
				_message = exception.InnerException.Message ?? "系统异常";
			}

			return Fail().SetMessage(_message);
		}

		public virtual void Clear()
		{
			Success = false;
			Body = default;
			Code = string.Empty;
			Message = string.Empty;
		}

		/// <summary>
		/// 根据返回状态自动判断调用函数是 Succeed or Fail
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public virtual IResult<T> Judge(bool status)
		{
			return status ? Succeed() : Fail();
		}

	}
}
