using AzureDevOps.Infrastructure.Exceptions;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;

namespace AzureDevOps.Infrastructure.AzureDevOps.Connection
{
    public class AzureDevOpsConnectionProvider : IAzureDevOpsConnectionProvider
    {
        private IAzureDevOpsConnection _connection;

        public IAzureDevOpsConnection Connection 
        {
            get
            {
                if (_connection is null)
                    throw new ConnectionNotEstablishedException("Connection to Azure DevOps has not been established. " +
                                                                "Please connect to Azure DevOps by using the api/connect endpoint. " +
                                                                "Please provide the url and accessToken in the body.");
                return _connection;
            }
        }

        public void SaveConnection(IAzureDevOpsConnection connection) => _connection = connection;
    }
}
