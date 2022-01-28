using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace pan.web.Swagger
{
    public class CustomSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // https://www.cnblogs.com/huanfion/p/10712413.html

            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
            //var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            //if (descriptor != null && !descriptor.ControllerName.StartsWith("Weather"))
            //{
            //}

            //operation.Parameters ??= new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter()
            //{
            //    Name = "timestamp",
            //    In = ParameterLocation.Header,
            //    Description = "The timestamp of now",
            //    Required = true
            //});

            var allow = context.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>() != null;
            if (!allow)
            {
                // 获取是否添加登录特性
                var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                         .Union(context.MethodInfo.GetCustomAttributes(true))
                                         .OfType<AuthorizeAttribute>().Any();
                if (authAttributes)
                {
                    operation.Responses.Add("401", new OpenApiResponse { Description = "暂无访问权限" });
                    operation.Responses.Add("403", new OpenApiResponse { Description = "禁止访问" });
                }
            }

        }
    }
}
