using AutoMapper;
using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using Microsoft.Azure.Cosmos;

namespace DotnetTaskAPI.Services.Concrete
{
    public class WorkflowService : IWorkflowService
    {
        private readonly Container _cosmosContainer;
        private readonly IMapper _mapper;

        public WorkflowService(CosmosClient cosmosClient, IMapper mapper)
        {
            if (cosmosClient == null)
            {
                throw new ArgumentNullException(nameof(cosmosClient));
            }
            _cosmosContainer = cosmosClient.GetContainer("DotnetTaskDB", "Workflows"); ;
            _mapper = mapper;
        }

        public async Task<GenericResponse> AddAsync(WorkflowDTO workflow)
        {
            try
            {
                var _workflow = _mapper.Map<Workflow>(workflow);
                _workflow.Id = Guid.NewGuid().ToString();
                Stages stage = new()
                {
                    Name = "Applied"
                };
                _workflow.Stages.Add(stage);
                var result = await _cosmosContainer.CreateItemAsync<Workflow>(_workflow, new PartitionKey(_workflow.Id));
                return new GenericResponse { Data = result.Resource, Description = "workflow added successfully", Status = _Constants._SUCCESS_ };
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
                var result = await _cosmosContainer.DeleteItemAsync<Workflow>(id, new PartitionKey(id));
                return new GenericResponse { Description = "workflow was deleted successfully", Status = _Constants._SUCCESS_ };
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
                var response = await _cosmosContainer.ReadItemAsync<Workflow>(id, new PartitionKey(id));

                return new GenericResponse { Data = response.Resource, Description = "Workflow retrieved successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }

        public async Task<GenericResponse> UpdateAsync(string id, Workflow workflow)
        {
            try
            {
                var _workflow = _mapper.Map<Workflow>(workflow);
                var item = await _cosmosContainer.UpsertItemAsync<Workflow>(_workflow, new PartitionKey(id));
                return new GenericResponse { Data = item.Resource, Description = "workflow was updated successfully", Status = _Constants._SUCCESS_ };
            }
            catch (Exception ex)
            {
                return new GenericResponse { Description = $"{ex.Message}", Status = _Constants._FAILED_ };
            }
        }
    }
}