using AutoMapper;
using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using Microsoft.Azure.Cosmos;

namespace DotnetTaskAPI.Services.Concrete
{
    public class ApplicationService : IApplicationService
    {
        private readonly Container _cosmosContainer;
        private readonly IMapper _mapper;

        public ApplicationService(CosmosClient cosmosClient, IMapper mapper)
        {
            if (cosmosClient == null)
            {
                throw new ArgumentNullException(nameof(cosmosClient));
            }
            _cosmosContainer = cosmosClient.GetContainer("DotnetTaskDB", "Applications"); ;
            _mapper = mapper;
        }

        public async Task<GenericResponse> AddAsync(ApplicationDTO application)
        {
            try
            {
                var _application = _mapper.Map<Application>(application);
                _application.Id = Guid.NewGuid().ToString();
                var result = await _cosmosContainer.CreateItemAsync<Application>(_application, new PartitionKey(_application.Id));
                return new GenericResponse { Data = result.Resource, Description = "Application added successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }

        public async Task<GenericResponse> DeleteAsync(string id)
        {
            try
            {
                var result = await _cosmosContainer.DeleteItemAsync<Application>(id, new PartitionKey(id));
                return new GenericResponse { Description = "Application was deleted successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }

        public async Task<GenericResponse> GetByIdAsync(string id)
        {
            try
            {
                var response = await _cosmosContainer.ReadItemAsync<Application>(id, new PartitionKey(id));
                return new GenericResponse { Data = response.Resource, Description = "Application retrieved successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }

        public async Task<GenericResponse> UpdateAsync(string id, Application application)
        {
            try
            {
                var _application = _mapper.Map<Application>(application);
                var item = await _cosmosContainer.UpsertItemAsync<Application>(_application, new PartitionKey(id));
                return new GenericResponse { Data = item.Resource, Description = "Application was updated successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }
    }
}