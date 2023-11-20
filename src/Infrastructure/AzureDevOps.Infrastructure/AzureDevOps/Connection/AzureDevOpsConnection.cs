using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;

namespace AzureDevOps.Infrastructure.AzureDevOps.Connection
{
    public class AzureDevOpsConnection : VssConnection, IAzureDevOpsConnection
    {
        public AzureDevOpsConnection(Uri baseUrl, VssCredentials credentials) : base(baseUrl, credentials) { }
    }
}
