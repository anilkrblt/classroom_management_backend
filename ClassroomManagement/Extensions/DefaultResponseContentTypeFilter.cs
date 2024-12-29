using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClassroomManagement.Extensions
{
    public class DefaultResponseContentTypeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Responses.Any())
            {
                foreach (var response in operation.Responses)
                {
                    response.Value.Content = new Dictionary<string, OpenApiMediaType>
                {
                    { "application/json", new OpenApiMediaType() }
                };
                }
            }
        }
    }

}