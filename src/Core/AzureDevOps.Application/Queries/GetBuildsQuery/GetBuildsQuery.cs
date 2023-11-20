using AzureDevOps.Domain.Misc;
using MediatR;

namespace AzureDevOps.Application.Queries.GetBuildsQuery
{
    public class GetBuildsQuery : IRequest<GetBuildsQueryResponse>
    {
        public string ProjectName { get; }

        public int[] Definitions { get; }

        public BuildStatusFilter BuildStatusFilter { get; set; }

        public GetBuildsQuery(string projectName, int[] definitions, BuildStatusFilter buildStatusFilter)
        {
            ProjectName = projectName;
            Definitions = definitions;
            BuildStatusFilter = buildStatusFilter;
        }
    }
}
