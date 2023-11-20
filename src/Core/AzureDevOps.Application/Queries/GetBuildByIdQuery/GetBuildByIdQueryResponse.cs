using AzureDevOps.Domain.Entities;
using AzureDevOps.Domain.Response;

namespace AzureDevOps.Application.Queries.GetBuildByIdQuery
{
    public class GetBuildByIdQueryResponse : BaseResponse
    {
        public Build Value { get; set; }
    }
}
