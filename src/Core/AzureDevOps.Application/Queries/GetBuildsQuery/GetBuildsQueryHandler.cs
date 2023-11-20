using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;

namespace AzureDevOps.Application.Queries.GetBuildsQuery
{
    public class GetBuildsQueryHandler : IRequestHandler<GetBuildsQuery, GetBuildsQueryResponse>
    {
        private readonly IBuildService _buildService;

        public GetBuildsQueryHandler(IBuildService buildService)
        {
            _buildService = buildService;
        }

        public async Task<GetBuildsQueryResponse> Handle(GetBuildsQuery request, CancellationToken cancellationToken)
        {
            return new GetBuildsQueryResponse()
            {
                Value = await _buildService.GetBuildsAsync(request.ProjectName, request.Definitions, request.BuildStatusFilter)
            };
        }
    }
}
