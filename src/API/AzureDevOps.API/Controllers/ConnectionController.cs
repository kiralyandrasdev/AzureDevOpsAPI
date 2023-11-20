using AzureDevOps.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AzureDevOps.Application.Commands.ConnectToAzureDevOpsCommand;

namespace AzureDevOps.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConnectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("connect")]
        public async Task<ActionResult<ConnectToAzureDevOpsCommandResponse>> ConnectAsync([FromBody] ConnectToAzureDevOpsCommandOptions options)
        {
            var result = await _mediator.Send(new ConnectToAzureDevOpsCommand(options));
           
            return result.MapActionResult();
        }
    }
}
