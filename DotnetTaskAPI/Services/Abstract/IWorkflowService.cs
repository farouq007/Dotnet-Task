using DotnetTaskAPI.Models;

namespace DotnetTaskAPI.Services.Abstract
{
    public interface IWorkflowService
    {
        Task<GenericResponse> AddAsync(WorkflowDTO workflow);

        Task<GenericResponse> DeleteAsync(string id);

        Task<GenericResponse> GetByIdAsync(string id);

        Task<GenericResponse> UpdateAsync(string id, Workflow workflow);
    }
}