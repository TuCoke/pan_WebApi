using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pan.web.Filters
{
    public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // debug 此处先注释
            //return;

            if (context.HttpContext.User.Identity.IsAuthenticated || hasAllowAnonymous(context))
            {
                return;
            }

            context.Result = new ContentResult() { Content = "access denined" };
        }

        /// <summary>
        /// 用于判断有没有AllowAnonymous标签
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool hasAllowAnonymous(AuthorizationFilterContext context)
        {
            var filters = context.Filters;
            for (var i = 0; i < filters.Count; i++)
            {
                if (filters[i] is IAllowAnonymousFilter)
                {
                    return true;
                }
            }

            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return true;
            }

            return false;
        }
    }
}
