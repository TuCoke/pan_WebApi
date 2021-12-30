using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pan.web.BaseControllers;
using Pan.Domain.Handler.Post;
using Pan.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.Controllers
{
    public class PostController: BaseController
    {
        public PostController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseValue<PagedResultDto<PostItmes>>))]
        public async Task<IActionResult> Login([FromQuery] PostRequest request)
        {
            var result = await _mediator.Send(request);
            return Json(new ResponseValue<PagedResultDto<PostItmes>>(result));
        }
    }
}
