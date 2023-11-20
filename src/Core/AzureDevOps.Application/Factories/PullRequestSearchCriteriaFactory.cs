using System;
using System.Collections.Generic;
using AzureDevOps.Domain.Misc;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace AzureDevOps.Application.Factories
{
    public class PullRequestSearchCriteriaFactory
    {
        private static readonly IDictionary<string, PullRequestStatus> _statusDictionary = new Dictionary<string, PullRequestStatus>
        {
            { "", PullRequestStatus.All },
            { "all", PullRequestStatus.All },
            { "active", PullRequestStatus.Active },
            { "completed", PullRequestStatus.Completed },
            { "abandoned", PullRequestStatus.Abandoned },
            { "notset", PullRequestStatus.NotSet }
        };  

        public static PullRequestSearchCriteria Create(string pullRequestStatus)
        {
            if (pullRequestStatus is null)
                pullRequestStatus = string.Empty;

            if (_statusDictionary.TryGetValue(pullRequestStatus.ToLower(), out var status))
                return new PullRequestSearchCriteria { Status = status };

            throw new ArgumentException($"Pull request search criteria not supported: {pullRequestStatus}");
        }
    }
}