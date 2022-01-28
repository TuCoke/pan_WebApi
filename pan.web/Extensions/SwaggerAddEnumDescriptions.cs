using Microsoft.OpenApi.Models;
using Pan.Common;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace pan.web.Extensions
{
	public class SwaggerAddEnumDescriptions : IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			var assembly = Assembly.Load("Pan.Infrastructure");
			foreach (var schema in swaggerDoc.Components.Schemas.Where(x => x.Value.Enum.Any()))
			{
				var type = assembly.GetType(schema.Key);
				if (type == null) continue;
				foreach (var value in Enum.GetValues(type))
				{
					var desc = value.GetDescription();
					if (string.IsNullOrEmpty(desc)) desc = value.ToString();
					schema.Value.Description += $"{desc}={(int)value},";
				}
			}
		}
	}
}
