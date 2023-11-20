using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureDevOps.Domain.Entities;
using AzureDevOps.Domain.Misc;

namespace AzureDevOps.Application.Interfaces.Infrastructure.Services
{
    public interface IGitService
    {
        Task<IEnumerable<PullRequest>> GetPullRequestsAsync(Guid repositoryId, PullRequestSearchCriteria pullRequestSearchCriteria);
    }
}
