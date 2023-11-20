using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.Build.WebApi;
using AzureDevOps.Domain.Misc;

namespace AzureDevOps.Application.Factories
{
    public class BuildStatusFilterFactory
    {
        private static readonly IDictionary<string, BuildStatus> _filterDictionary = new Dictionary<string, BuildStatus>
        {
            { "", BuildStatus.All },
            { "all", BuildStatus.All },
            { "completed", BuildStatus.Completed },
            { "inprogress", BuildStatus.InProgress },
            { "cancelling", BuildStatus.Cancelling },
            { "notstarted", BuildStatus.NotStarted },
            { "postponed", BuildStatus.Postponed },
            { "none", BuildStatus.None },
        };

        public static BuildStatusFilter Create(string buildStatus)
        {
            if (buildStatus is null)
                buildStatus = string.Empty;

            if (_filterDictionary.TryGetValue(buildStatus.ToLower(), out var status))
                return new BuildStatusFilter { BuildStatus = status };

            throw new ArgumentException($"Build status filter not supported: {buildStatus}");
        }
    }
}
