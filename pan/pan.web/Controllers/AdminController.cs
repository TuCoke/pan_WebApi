using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pan.web.BaseControllers;
using Pan.Domain.Handler.Accounts;
using Pan.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.Controllers
{
    public class AdminController: BaseController
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }


		/// <summary>
		/// 管理员登录
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseValue<AdminLoginResponse>))]
		public async Task<IActionResult> Login([FromBody] AdminLoginRequest request)
		{
			var result = await _mediator.Send(request);
			return Json(new ResponseValue<AdminLoginResponse>(result));
		}

		/// <summary>
		/// 创建管理员
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseValue))]
		public async Task<IActionResult> Create([FromBody] CreateAdminRequest request)
		{
			var result = await _mediator.Send(request);

			if (result) return Json(new ResponseValue());
			else return Json(new ResponseValue("操作未完成"));
		}
	}
}
