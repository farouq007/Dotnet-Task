using DotnetTaskAPI.Models;

namespace DotnetTaskAPI.Services.Abstract
{
    public interface IApplicationService
    {
        Task<GenericResponse> DeleteAsync(string id);

        Task<GenericResponse> AddAsync(ApplicationDTO entity);

        Task<GenericResponse> GetByIdAsync(string id);

        Task<GenericResponse> UpdateAsync(string id, Application application);
    }
}