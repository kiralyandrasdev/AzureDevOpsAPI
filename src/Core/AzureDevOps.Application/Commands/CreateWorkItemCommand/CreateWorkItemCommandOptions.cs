using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AzureDevOps.Domain.Misc;

namespace AzureDevOps.Application.Commands.CreateWorkItemCommand
{
    public class CreateWorkItemCommandOptions
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string WorkItemType { get; set; }

        public string Description { get; set; }

        public string AreaPath { get; set; }

        public string Tags { get; set; }

        public string Comment { get; set; }

        public IEnumerable<Attachment> Attachments { get; set; } = new List<Attachment>();

        public IEnumerable<Relation> ChildRelations { get; set; } = new List<Relation>();

        public IEnumerable<Relation> ParentRelations { get; set; } = new List<Relation>();
    }
}
