using System.Threading;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;
using AzureDevOps.Domain.Entities;
using MediatR;

namespace AzureDevOps.Application.Commands.CreateWorkItemCommand
{
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, CreateWorkItemCommandResponse>
    {
        private readonly IWorkItemService _workItemService;

        public CreateWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task<CreateWorkItemCommandResponse> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var result = new CreateWorkItemCommandResponse();

            var workItemToBeCreated = new WorkItem()
            {
                Title = request.Options.Title,
                WorkItemType = request.Options.WorkItemType,
                Description = request.Options.Description,
                AreaPath = request.Options.AreaPath,
                Tags = request.Options.Tags,
                Comment = request.Options.Comment,
                Attachments = request.Options.Attachments,
                ChildRelations = request.Options.ChildRelations,
                ParentRelations = request.Options.ParentRelations
            };

            result.Value = await _workItemService.CreateWorkItemAsync(request.ProjectName, workItemToBeCreated);

            return result;
        }
    }
}
