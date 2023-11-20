using System;

namespace AzureDevOps.Domain.Entities
{
    public class Build : IEntity
    {
        public int Id { get; set; }

        public string SourceBranch { get; set; }

        public bool IsScheduled { get; set; }

        public DateTime? StartTime { get; set; }

        public string BuildUrl { get; set; }
    }
}
