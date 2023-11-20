namespace AzureDevOps.Application.Interfaces.Infrastructure.Connection
{
    public interface IAzureDevOpsConnectionProvider
    {
        IAzureDevOpsConnection Connection { get; }

        void SaveConnection(IAzureDevOpsConnection connection);
    }
}
