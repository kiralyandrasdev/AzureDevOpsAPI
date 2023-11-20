using AzureDevOps.API.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AzureDevOps.API.Middlewares
{
    public class AuthorizationMiddleware : IMiddleware
    {
        private readonly IHostingEnvironment _env;

        public AuthorizationMiddleware(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_env.IsDevelopment())
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.ContainsKey("Authorization"))
                throw new AuthorizationFailedException("Authorization header is missing.");

            string authorizationHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                throw new AuthorizationFailedException("Authorization header is empty.");

            await next(context);
        }
    }
}
