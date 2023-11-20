using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AzureDevOps.API.Extensions;
using AzureDevOps.Application.Factories;
using AzureDevOps.Application.Queries.GetBuildByIdQuery;
using AzureDevOps.Application.Queries.GetBuildsQuery;

namespace AzureDevOps.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class BuildController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BuildController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{projectName}/builds/{buildId}")]
        public async Task<ActionResult<GetBuildByIdQueryResponse>> GetBuildByIdAsync(string projectName, int buildId)
        {
            var result = await _mediator.Send(new GetBuildByIdQuery(projectName, buildId));

            return result.MapActionResult();
        }

        [HttpGet]
        [Route("{projectName}/{buildDefinitionId}/builds")]
        public async Task<ActionResult<GetBuildsQueryResponse>> GetBuildsAsync(
            string projectName, 
            int buildDefinitionId, 
            [FromQuery(Name = "$filter")] string buildStatus)
        {
            var buildStatusFilter = BuildStatusFilterFactory.Create(buildStatus);

            var result = await _mediator.Send(new GetBuildsQuery(projectName, new int[] { buildDefinitionId }, buildStatusFilter));

            return result.MapActionResult();
        }
    }
}
