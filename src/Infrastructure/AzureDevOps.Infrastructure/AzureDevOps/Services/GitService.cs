using Microsoft.TeamFoundation.SourceControl.WebApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AzureDevOps.Domain.Misc;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;
using AzureDevOps.Domain.Entities;

namespace AzureDevOps.Infrastructure.AzureDevOps.Services
{
    public class GitService : IGitService
    {
        private readonly GitHttpClient _gitHttpClient;

        public GitService(IAzureDevOpsConnectionProvider provider)
        {
            _gitHttpClient = provider.Connection.GetClient<GitHttpClient>();
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequestsAsync(Guid repositoryId, PullRequestSearchCriteria pullRequestSearchCriteria)
        {
            var pullRequests = await _gitHttpClient.GetPullRequestsAsync(
                repositoryId,
                new GitPullRequestSearchCriteria { Status = pullRequestSearchCriteria.Status });

            return pullRequests
                .Select(p =>
                new PullRequest
                { 
                    Id = p.PullRequestId, 
                    Title = p.Title,
                    TargetRefName = p.TargetRefName,
                    Status = p.Status.ToString(),
                    MergeStatus = p.MergeStatus.ToString(),
                    ClosedDate = p.ClosedDate
                }).OrderBy(p => p.Id);
        }
    }
}
