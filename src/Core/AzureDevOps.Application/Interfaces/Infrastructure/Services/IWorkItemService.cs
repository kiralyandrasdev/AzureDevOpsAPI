using System.Threading.Tasks;
using WorkItem = AzureDevOps.Domain.Entities.WorkItem;

namespace AzureDevOps.Application.Interfaces.Infrastructure.Services
{
    public interface IWorkItemService
    {
        Task<WorkItem> CreateWorkItemAsync(string projectName, WorkItem workItem);

        Task<WorkItem> GetWorkItemByIdAsync(int workItemId);

        Task DeleteWorkItemAsync(int workItemId);
    }
}
