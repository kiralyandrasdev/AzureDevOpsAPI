using AzureDevOps.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AzureDevOps.Application.Commands.CreateWorkItemCommand;
using AzureDevOps.Application.Commands.DeleteWorkItemCommand;
using AzureDevOps.Application.Queries.GetWorkItemByIdQuery;

namespace AzureDevOps.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("{projectName}/workitems")]
        public async Task<ActionResult<CreateWorkItemCommandResponse>> CreateWorkItemAsync(
            string projectName,
            [FromBody] CreateWorkItemCommandOptions options)
        {
            var result = await _mediator.Send(new CreateWorkItemCommand(projectName, options));

            return result.MapActionResult();
        }

        [HttpGet]
        [Route("workitems/{workItemId}")]
        public async Task<ActionResult<GetWorkItemByIdQueryResponse>> GetWorkItemByIdAsync(
            int workItemId)
        {
            var result = await _mediator.Send(new GetWorkItemByIdQuery(workItemId));

            return result.MapActionResult();
        }

        [HttpDelete]
        [Route("workitems/{workItemId}")]
        public async Task<ActionResult<DeleteWorkItemCommandResponse>> DeleteWorkItemAsync(
           int workItemId)
        {
            var result = await _mediator.Send(new DeleteWorkItemCommand(workItemId));

            return result.MapActionResult();
        }
    }
}
