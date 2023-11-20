using AzureDevOps.Domain.Misc;
using System;
using System.Threading.Tasks;
using AzureDevOps.Infrastructure.Utils;
using Microsoft.VisualStudio.Services.Common;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;

namespace AzureDevOps.Infrastructure.AzureDevOps.Connection
{
    public class AzureDevOpsConnectionService : IAzureDevOpsConnectionService
    {
        private const string ConnectionSuccessfullyEstablishedMessage = "Connection to Azure DevOps successfully established.";
        private const string ConnectionFailedMessage = "Connection to Azure DevOps failed.";

        private readonly IAzureDevOpsConnectionProvider _connectionProvider;

        public AzureDevOpsConnectionService(IAzureDevOpsConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IAzureDevOpsConnection> ConnectToAzureDevOpsAsync(string url, string accessToken)
        {
            ArgumentUtils.ThrowIfNull(url, nameof(url));
            ArgumentUtils.ThrowIfNull(accessToken, nameof(accessToken));

            var connection = new AzureDevOpsConnection(new Uri(url), new VssBasicCredential(string.Empty, accessToken));
            await connection.ConnectAsync();

            return connection;
        }

        public AzureDevOpsConnectionValidationResult ValidateAzureDevOpsConnection(IAzureDevOpsConnection connection)
        {
            var connectionSuccessful = connection != null && connection.HasAuthenticated;

            return new AzureDevOpsConnectionValidationResult()
            {
                Success = connectionSuccessful,
                Message = connectionSuccessful ? ConnectionSuccessfullyEstablishedMessage : ConnectionFailedMessage
            };
        }

        public void SaveAzureDevOpsConnection(IAzureDevOpsConnection connection)
            => _connectionProvider.SaveConnection(connection);
    }
}