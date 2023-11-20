using System.Threading;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;
using MediatR;

namespace AzureDevOps.Application.Commands.ConnectToAzureDevOpsCommand
{
    public class ConnectToAzureDevOpsCommandHandler : IRequestHandler<ConnectToAzureDevOpsCommand, ConnectToAzureDevOpsCommandResponse>
    {
        private readonly IAzureDevOpsConnectionService _connectionService;

        public ConnectToAzureDevOpsCommandHandler(IAzureDevOpsConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<ConnectToAzureDevOpsCommandResponse> Handle(ConnectToAzureDevOpsCommand request, CancellationToken cancellationToken)
        {
            var result = new ConnectToAzureDevOpsCommandResponse();

            var connection = await _connectionService.ConnectToAzureDevOpsAsync(request.Options.Url, request.Options.AccessToken);

            var validation = _connectionService.ValidateAzureDevOpsConnection(connection);

            if (validation.Success)
            {
                _connectionService.SaveAzureDevOpsConnection(connection);
                result.Value = validation.Message;
            }
            else
                result.ErrorMessage = validation.Message;

            return result;
        }
    }
}
