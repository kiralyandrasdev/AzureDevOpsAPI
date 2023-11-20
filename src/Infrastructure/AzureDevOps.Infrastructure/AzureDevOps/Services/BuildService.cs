using AzureDevOps.Domain.Misc;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;
using Build = AzureDevOps.Domain.Entities.Build;

namespace AzureDevOps.Infrastructure.AzureDevOps.Services
{
    public class BuildService : IBuildService
    {
        private readonly BuildHttpClient _buildHttpClient;

        public BuildService(IAzureDevOpsConnectionProvider provider)
        {
            _buildHttpClient = provider.Connection.GetClient<BuildHttpClient>();
        }

        public async Task<Build> GetBuildByIdAsync(string projectName, int buildId)
        {
            var build = await _buildHttpClient.GetBuildAsync(projectName, buildId);

            return new Build
            {
                Id = build.Id,
                SourceBranch = build.SourceBranch,
                IsScheduled = build.Reason == BuildReason.Schedule,
                StartTime = build.StartTime,
                BuildUrl = (build.Links.Links["web"] as ReferenceLink)?.Href
            };
        }

        public async Task<IEnumerable<Build>> GetBuildsAsync(string projectName, int[] definitions, BuildStatusFilter buildStatusFilter)
        {
            var builds = await _buildHttpClient.GetBuildsAsync(
                projectName,
                definitions: definitions,
                statusFilter: buildStatusFilter.BuildStatus);

            return builds
                .Select(b => new Build
                {
                    Id = b.Id,
                    SourceBranch = b.SourceBranch,
                    IsScheduled = b.Reason == BuildReason.Schedule,
                    StartTime = b.StartTime,
                    BuildUrl = (b.Links.Links["web"] as ReferenceLink)?.Href
                })
                .OrderBy(b => b.Id);
        }
    }
}
