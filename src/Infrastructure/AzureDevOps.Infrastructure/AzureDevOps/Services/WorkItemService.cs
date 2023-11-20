using System.Collections.Generic;
using System.IO;
using AzureDevOps.Domain.Misc;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using System.Linq;
using System.Threading.Tasks;
using AzureDevOps.Infrastructure.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;
using AzureDevOps.Application.Interfaces.Infrastructure.Services;
using WorkItem = AzureDevOps.Domain.Entities.WorkItem;

namespace AzureDevOps.Infrastructure.AzureDevOps.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly WorkItemTrackingHttpClient _workItemHttpClient;

        public WorkItemService(IAzureDevOpsConnectionProvider provider)
        {
            _workItemHttpClient = provider.Connection.GetClient<WorkItemTrackingHttpClient>();
        }

        public async Task<WorkItem> CreateWorkItemAsync(string projectName, WorkItem workItem)
        {
            var jsonPatchDocument = await CreateJsonPatchDocumentAsync(projectName, workItem);

            var createdWorkItem = await _workItemHttpClient.CreateWorkItemAsync(jsonPatchDocument, projectName, workItem.WorkItemType);

            return await GetWorkItemByIdAsync(createdWorkItem.Id.Value);
        }

        public async Task<WorkItem> GetWorkItemByIdAsync(int workItemId)
        {
            var workItem = await _workItemHttpClient.GetWorkItemAsync(workItemId, expand: WorkItemExpand.All);

            var childRelations = workItem.Relations?
                .Where(r => r.Rel == "System.LinkTypes.Hierarchy-Forward")
                .Select(relation => new Relation { Url = relation.Url });

            var parentRelations = workItem.Relations?
                .Where(r => r.Rel == "System.LinkTypes.Hierarchy-Reverse")
                .Select(relation => new Relation { Url = relation.Url });

            var attachments = workItem.Relations?
                .Where(r => r.Rel == "AttachedFile")
                .Select(relation => new Attachment { Url = relation.Url });

            var description = workItem.Fields.TryGetValue("System.Description", out var desc)
                ? desc.ToString()
                : string.Empty;

            var firstComment = workItem.Fields.TryGetValue("System.History", out var comment)
                ? comment.ToString()
                : string.Empty;

            return new WorkItem
            {
                Id = workItemId,
                Title = workItem.Fields["System.Title"].ToString(),
                WorkItemType = workItem.Fields["System.WorkItemType"].ToString(),
                AreaPath = workItem.Fields["System.AreaPath"].ToString(),
                Url = workItem.Url,
                ChildRelations = childRelations,
                ParentRelations = parentRelations,
                Attachments = attachments,
                Description = description.SanitizeHtmlString().Trim(' '),
                Tags = workItem.Fields.TryGetValue("System.Tags", out var tags) ? tags.ToString() : string.Empty,
                CommentCount = int.Parse(workItem.Fields["System.CommentCount"].ToString()),
                Comment = firstComment.SanitizeHtmlString()
            };
        }

        public async Task DeleteWorkItemAsync(int workItemId)
            => await _workItemHttpClient.DeleteWorkItemAsync(workItemId);

        private async Task<JsonPatchDocument> CreateJsonPatchDocumentAsync(string projectName, WorkItem workItem)
        {
            var patchDocument = new JsonPatchDocument
            {
                CreateJsonPatchOperation(Operation.Add, "/fields/System.Title", workItem.Title)
            };

            if (!string.IsNullOrEmpty(workItem.Description))
                patchDocument.Add(CreateJsonPatchOperation(Operation.Add, "/fields/System.Description", workItem.Description));

            if (!string.IsNullOrEmpty(workItem.AreaPath))
                patchDocument.Add(CreateJsonPatchOperation(Operation.Add, "/fields/System.AreaPath", workItem.AreaPath));

            if (!string.IsNullOrEmpty(workItem.Tags))
                patchDocument.Add(CreateJsonPatchOperation(Operation.Add, "/fields/System.Tags", workItem.Tags));

            if (!string.IsNullOrEmpty(workItem.Comment))
                patchDocument.Add(CreateJsonPatchOperation(Operation.Add, "/fields/System.History", workItem.Comment));

            patchDocument.AddRange(CreateRelationJsonPatchOperationList("System.LinkTypes.Hierarchy-Forward", workItem.ChildRelations));
            patchDocument.AddRange(CreateRelationJsonPatchOperationList("System.LinkTypes.Hierarchy-Reverse", workItem.ParentRelations));

            foreach (var attachment in workItem.Attachments)
            {
                await CreateAttachmentsAsync(projectName, attachment, patchDocument);
            }

            return patchDocument;
        }

        private static IEnumerable<JsonPatchOperation> CreateRelationJsonPatchOperationList(string relation, IEnumerable<Relation> relations)
        {
            return relations.Select(r => CreateJsonPatchOperation(Operation.Add, "/relations/-", new { rel = relation, url = r.Url }));
        }

        private async Task CreateAttachmentsAsync(string projectName, Attachment attachment, JsonPatchDocument patchDocument)
        {
            using (var stream = new FileStream(attachment.AttachmentFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var attachmentReference = await _workItemHttpClient.CreateAttachmentAsync(
                    stream,
                    projectName,
                    attachment.AttachmentFileName,
                    null,
                    null);

                patchDocument.Add(CreateJsonPatchOperation(
                    Operation.Add,
                    "/relations/-",
                    new
                    {
                        rel = "AttachedFile",
                        url = attachmentReference.Url,
                    }));
            }
        }

        private static JsonPatchOperation CreateJsonPatchOperation(Operation operation, string path, object value)
        {
            return new JsonPatchOperation
            {
                Operation = operation,
                Path = path,
                Value = value
            };
        }
    }
}
