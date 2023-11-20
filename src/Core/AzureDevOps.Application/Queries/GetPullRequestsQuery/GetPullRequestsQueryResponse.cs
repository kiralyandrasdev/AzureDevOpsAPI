using System.Collections.Generic;
using AzureDevOps.Domain.Entities;
using AzureDevOps.Domain.Response;

namespace AzureDevOps.Application.Queries.GetPullRequestsQuery
{
    public class GetPullRequestsQueryResponse : BaseResponse
    {
        public IEnumerable<PullRequest> Value { get; set; }
    }
}
