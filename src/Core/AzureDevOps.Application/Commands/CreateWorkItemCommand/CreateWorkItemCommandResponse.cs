using AzureDevOps.Domain.Entities;
using AzureDevOps.Domain.Response;

namespace AzureDevOps.Application.Commands.CreateWorkItemCommand
{
    public class CreateWorkItemCommandResponse : BaseResponse
    {
        public WorkItem Value { get; set; }
    }
}
