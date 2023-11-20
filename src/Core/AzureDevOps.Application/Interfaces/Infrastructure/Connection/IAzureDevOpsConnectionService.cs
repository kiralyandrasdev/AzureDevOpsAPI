using System.Threading.Tasks;
using AzureDevOps.Domain.Misc;

namespace AzureDevOps.Application.Interfaces.Infrastructure.Connection
{
    public interface IAzureDevOpsConnectionService
    {
        Task<IAzureDevOpsConnection> ConnectToAzureDevOpsAsync(string url, string accessToken);

        AzureDevOpsConnectionValidationResult ValidateAzureDevOpsConnection(IAzureDevOpsConnection connection);

        void SaveAzureDevOpsConnection(IAzureDevOpsConnection connection);
    }
}