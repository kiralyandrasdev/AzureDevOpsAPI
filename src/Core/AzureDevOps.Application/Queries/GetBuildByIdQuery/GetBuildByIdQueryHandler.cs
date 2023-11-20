using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;

namespace AzureDevOps.Application.Queries.GetBuildByIdQuery
{
    public class GetBuildByIdQueryHandler : IRequestHandler<GetBuildByIdQuery, GetBuildByIdQueryResponse>
    {
        private readonly IBuildService _buildService;

        public GetBuildByIdQueryHandler(IBuildService buildService)
        {
            _buildService = buildService;
        }

        public async Task<GetBuildByIdQueryResponse> Handle(GetBuildByIdQuery request, CancellationToken cancellationToken)
            => new GetBuildByIdQueryResponse { Value = await _buildService.GetBuildByIdAsync(request.ProjectName, request.BuildId) };
    }
}
