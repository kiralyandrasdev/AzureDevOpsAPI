using AzureDevOps.Application.Interfaces.Infrastructure.Connection;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;
using AzureDevOps.Infrastructure.AzureDevOps.Connection;
using AzureDevOps.Infrastructure.AzureDevOps.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOps.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IAzureDevOpsConnectionService, AzureDevOpsConnectionService>();
            services.AddSingleton<IAzureDevOpsConnectionProvider, AzureDevOpsConnectionProvider>();
            services.AddSingleton<IGitService, GitService>();
            services.AddSingleton<IBuildService, BuildService>();
            services.AddSingleton<IWorkItemService, WorkItemService>();

            return services;
        }
    }
}
