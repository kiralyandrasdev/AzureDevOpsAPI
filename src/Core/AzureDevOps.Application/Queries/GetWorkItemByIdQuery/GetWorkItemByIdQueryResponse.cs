using AzureDevOps.Domain.Entities;
using AzureDevOps.Domain.Response;

namespace AzureDevOps.Application.Queries.GetWorkItemByIdQuery
{
    public class GetWorkItemByIdQueryResponse : BaseResponse
    {
        public WorkItem Value { get; set; }
    }
}
