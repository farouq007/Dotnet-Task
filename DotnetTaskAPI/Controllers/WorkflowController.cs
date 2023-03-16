using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using DotnetTaskAPI.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using static System.Net.Mime.MediaTypeNames;

namespace DotnetTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _workflow;
        private readonly IProgramService _programService;

        public WorkflowController(IWorkflowService WorkflowService, IProgramService programService)
        {
            _workflow = WorkflowService;
            _programService = programService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _workflow.GetByIdAsync(id);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(WorkflowDTO Workflow)
        {
            var program = await _programService.GetByIdAsync(Workflow.ProgramId);
            if (program.Status == _Constants._FAILED_) return BadRequest("The Program you're trying to add an application for does not exist");
            var result = await _workflow.AddAsync(Workflow);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Workflow Workflow)
        {
            var result = await _workflow.UpdateAsync(Workflow.Id, Workflow);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _workflow.DeleteAsync(id);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }
    }
}