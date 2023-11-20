using System.Collections.Generic;
using AzureDevOps.Domain.Misc;

namespace AzureDevOps.Domain.Entities
{
    public class WorkItem : IEntity
    {
        public int Id { get; set; }

        public string WorkItemType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AreaPath { get; set; }

        public string Tags { get; set; }

        public int CommentCount { get; set; }

        public string Comment { get; set; }

        public string Url { get; set; }

        public IEnumerable<Attachment> Attachments { get; set; }

        public IEnumerable<Relation> ParentRelations { get; set; }

        public IEnumerable<Relation> ChildRelations { get; set; }
    }
}
