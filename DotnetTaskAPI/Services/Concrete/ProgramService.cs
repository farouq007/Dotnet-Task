using AutoMapper;
using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace DotnetTaskAPI.Services.Concrete
{
    public class ProgramService : IProgramService
    {
        private readonly Container _cosmosContainer;
        private readonly IMapper _mapper;

        public ProgramService(CosmosClient cosmosClient, IMapper mapper)
        {
            if (cosmosClient == null)
            {
                throw new ArgumentNullException(nameof(cosmosClient));
            }
            _cosmosContainer = cosmosClient.GetContainer("DotnetTaskDB", "Programs");
            _mapper = mapper;
        }

        public async Task<GenericResponse> AddAsync(ProgramDetailsDTO programDetails)
        {
            try
            {
                var _programDetails = _mapper.Map<ProgramDetails>(programDetails);
                _programDetails.Id = Guid.NewGuid().ToString();
                var result = await _cosmosContainer.CreateItemAsync<ProgramDetails>(_programDetails, new PartitionKey(_programDetails.Id));
                return new GenericResponse { Data = result.Resource, Description = "Program added successfully", Status = _Constants._SUCCESS_ };
            }
            catch (CosmosException ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }

        public async Task<GenericResponse> DeleteAsync(string id)
        {
            try
            {
                var result = await _cosmosContainer.DeleteItemAsync<ProgramDetails>(id, new PartitionKey(id));
                return new GenericResponse { Description = "Program was deleted successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }

        public async Task<GenericResponse> GetAllAsync()
        {
            try
            {
                var sqlCosmosQuery = "Select * from c";
                List<ProgramDetails> result = new();
                var query = _cosmosContainer.GetItemQueryIterator<ProgramDetails>(new QueryDefinition(sqlCosmosQuery));
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    result.AddRange(response);
                }
                return new GenericResponse { Data = result, Description = "All programs were retrieved successfully", Status = _Constants._SUCCESS_ };
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
                var response = await _cosmosContainer.ReadItemAsync<ProgramDetails>(id, new PartitionKey(id));
                return new GenericResponse { Data = response.Resource, Description = "Program retrieved successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }

        public async Task<GenericResponse> UpdateAsync(string id, ProgramDetails programDetails)
        {
            try
            {
                var _programDetails = _mapper.Map<ProgramDetails>(programDetails);
                var item = await _cosmosContainer.UpsertItemAsync<ProgramDetails>(_programDetails, new PartitionKey(id));
                return new GenericResponse { Data = item.Resource, Description = "Program was updated successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }
    }
}