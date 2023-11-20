using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;

namespace AzureDevOps.Application.Commands.DeleteWorkItemCommand
{
    internal class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, DeleteWorkItemCommandResponse>
    {
        private readonly IWorkItemService _workItemService;

        public DeleteWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task<DeleteWorkItemCommandResponse> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.DeleteWorkItemAsync(request.WorkItemId);

            return new DeleteWorkItemCommandResponse();
        }
    }
}
