using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;

namespace AzureDevOps.Application.Queries.GetWorkItemByIdQuery
{
    public class GetWorkItemByIdQueryHandler : IRequestHandler<GetWorkItemByIdQuery, GetWorkItemByIdQueryResponse>
    {
        private readonly IWorkItemService _workItemService;

        public GetWorkItemByIdQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task<GetWorkItemByIdQueryResponse> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
            => new GetWorkItemByIdQueryResponse { Value = await _workItemService.GetWorkItemByIdAsync(request.WorkItemId) };
    }
}
