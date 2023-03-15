using DotnetTaskAPI.Models;

namespace DotnetTaskAPI.Services.Abstract
{
    public interface IProgramService
    {
        Task<GenericResponse> GetAllAsync();

        Task<GenericResponse> DeleteAsync(string id);

        Task<GenericResponse> AddAsync(ProgramDetailsDTO entity);

        Task<GenericResponse> GetByIdAsync(string id);

        Task<GenericResponse> UpdateAsync(string id, ProgramDetails programDetails);
    }
}