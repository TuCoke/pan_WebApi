using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.BaseControllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]/[action]")]
	public class BaseController : Controller
	{
		protected readonly IMediator _mediator;

		public BaseController(IMediator mediator)
		{
			_mediator = mediator;
		}
	}
}
