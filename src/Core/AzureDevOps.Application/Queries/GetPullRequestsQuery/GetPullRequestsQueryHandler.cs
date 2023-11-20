using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;

namespace AzureDevOps.Application.Queries.GetPullRequestsQuery
{
    public class GetPullRequestsQueryHandler : IRequestHandler<GetPullRequestsQuery, GetPullRequestsQueryResponse>
    {
        private readonly IGitService _gitService;

        public GetPullRequestsQueryHandler(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task<GetPullRequestsQueryResponse> Handle(GetPullRequestsQuery request, CancellationToken cancellationToken)
        {
            return new GetPullRequestsQueryResponse
            {
                Value = await _gitService.GetPullRequestsAsync(request.RepositoryId, request.SearchCriteria)
            };
        }
    }
}
