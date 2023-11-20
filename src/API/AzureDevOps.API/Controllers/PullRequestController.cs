using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MediatR;
using AzureDevOps.API.Extensions;
using AzureDevOps.Application.Factories;
using AzureDevOps.Application.Queries.GetPullRequestsQuery;

namespace AzureDevOps.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class PullRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PullRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{repositoryId}/pullrequests")]
        public async Task<ActionResult<GetPullRequestsQueryResponse>> GetPullRequestsAsync(
            Guid repositoryId, 
            [FromQuery(Name = "$filter")] string pullRequestStatus)
        {
            var searchCriteria = PullRequestSearchCriteriaFactory.Create(pullRequestStatus);

            var result = await _mediator.Send(new GetPullRequestsQuery(repositoryId, searchCriteria));

            return result.MapActionResult();
        }
    }
}
