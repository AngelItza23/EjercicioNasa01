using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class SwaggerFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var parameter in operation.Parameters)
        {
            if (parameter.In == ParameterLocation.Query)
            {
                continue;
            }
        }

        if (context.ApiDescription.HttpMethod == "POST" || context.ApiDescription.HttpMethod == "PUT")
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["archivo"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            },
                            Required = new HashSet<string> { "archivo" }
                        }
                    }
                }
            };
        }
    }
}

