using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.Application.Interfaces.Infrastructure.Connection
{
    public interface IAzureDevOpsConnection
    {
        bool HasAuthenticated { get; }

        T GetClient<T>() where T : VssHttpClientBase;
    }
}
