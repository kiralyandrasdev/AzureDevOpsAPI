using MediatR;

namespace AzureDevOps.Application.Queries.GetBuildByIdQuery
{
    public class GetBuildByIdQuery : IRequest<GetBuildByIdQueryResponse>
    {
        public string ProjectName { get; }

        public int BuildId { get; }

        public GetBuildByIdQuery(string projectName, int buildId)
        {
            ProjectName = projectName;
            BuildId = buildId;
        }
    }
}
