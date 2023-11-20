using MediatR;

namespace AzureDevOps.Application.Commands.CreateWorkItemCommand
{
    public class CreateWorkItemCommand : IRequest<CreateWorkItemCommandResponse>
    {
        public string ProjectName { get; }

        public CreateWorkItemCommandOptions Options { get; }

        public CreateWorkItemCommand(string projectName, CreateWorkItemCommandOptions options)
        {
            ProjectName = projectName;
            Options = options;
        }
    }
}
