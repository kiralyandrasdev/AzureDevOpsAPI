using System;

namespace AzureDevOps.Domain.Entities
{
    public class PullRequest : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TargetRefName { get; set; }

        public string Status { get; set; }

        public string MergeStatus { get; set; }

        public DateTime ClosedDate { get; set; }
    }
}
