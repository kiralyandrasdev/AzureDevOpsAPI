using MediatR;

namespace AzureDevOps.Application.Commands.DeleteWorkItemCommand
{
    public class DeleteWorkItemCommand : IRequest<DeleteWorkItemCommandResponse>
    {
        public int WorkItemId { get; }

        public DeleteWorkItemCommand(int workItemId)
        {
            WorkItemId = workItemId;
        }
    }
}
