using MediatR;

namespace AzureDevOps.Application.Queries.GetWorkItemByIdQuery
{
    public class GetWorkItemByIdQuery : IRequest<GetWorkItemByIdQueryResponse>
    {
        public int WorkItemId { get; }

        public GetWorkItemByIdQuery(int workItemId)
        {
            WorkItemId = workItemId;
        }
    }
}
