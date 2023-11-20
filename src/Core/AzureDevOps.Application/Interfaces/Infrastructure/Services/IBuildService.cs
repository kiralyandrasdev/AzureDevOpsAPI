using System.Collections.Generic;
using System.Threading.Tasks;
using AzureDevOps.Domain.Entities;
using AzureDevOps.Domain.Misc;

namespace AzureDevOps.Application.Interfaces.Infrastructure.Services
{
    public interface IBuildService
    {
        Task<Build> GetBuildByIdAsync(string projectName, int buildId);

        Task<IEnumerable<Build>> GetBuildsAsync(string projectName, int[] definitions, BuildStatusFilter buildStatusFilter);
    }
}
