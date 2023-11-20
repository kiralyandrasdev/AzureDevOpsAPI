using AzureDevOps.API.Settings;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using AzureDevOps.Domain.Response;

namespace AzureDevOps.API.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
			{
                var response = new BaseResponse
                {
                    ErrorType = ex.GetType().Name,
                    ErrorMessage = ex.Message
                };

                context.Response.StatusCode = 500;
                context.Response.Headers.Add("Content-Type", "application/json");

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new ApplicationJsonSerializerSettings()));
            }
        }
    }
}
