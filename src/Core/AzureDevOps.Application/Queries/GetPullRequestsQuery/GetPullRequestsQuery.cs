using MediatR;
using System;
using AzureDevOps.Domain.Misc;

namespace AzureDevOps.Application.Queries.GetPullRequestsQuery
{
    public class GetPullRequestsQuery : IRequest<GetPullRequestsQueryResponse>
    {
        public Guid RepositoryId { get; }

        public PullRequestSearchCriteria SearchCriteria { get; }

        public GetPullRequestsQuery(Guid repositoryId, PullRequestSearchCriteria searchCriteria)
        {
            RepositoryId = repositoryId;
            SearchCriteria = searchCriteria;
        }
    }
}
