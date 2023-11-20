using AzureDevOps.Application;
using AzureDevOps.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevOps.API.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult<T> MapActionResult<T>(this T response) where T : BaseResponse
        {
            if (!string.IsNullOrEmpty(response.ErrorMessage))
                return new BadRequestObjectResult(response);

            return new OkObjectResult(response);
        }
    }
}
