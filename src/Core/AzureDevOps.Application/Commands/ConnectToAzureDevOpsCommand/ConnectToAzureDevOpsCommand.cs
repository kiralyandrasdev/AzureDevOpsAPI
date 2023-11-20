using MediatR;

namespace AzureDevOps.Application.Commands.ConnectToAzureDevOpsCommand
{
    public class ConnectToAzureDevOpsCommand : IRequest<ConnectToAzureDevOpsCommandResponse>
    {
        public ConnectToAzureDevOpsCommandOptions Options { get; set; }

        public ConnectToAzureDevOpsCommand(ConnectToAzureDevOpsCommandOptions options)
        {
            Options = options;
        }
    }
}
