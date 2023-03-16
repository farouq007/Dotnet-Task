using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace DotnetTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IProgramService _programService;

        public ApplicationController(IApplicationService applicationService, IProgramService programService)
        {
            _applicationService = applicationService;
            _programService = programService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _applicationService.GetByIdAsync(id);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ApplicationDTO application)
        {
            var program = await _programService.GetByIdAsync(application.ProgramId);
            if (program.Status == _Constants._FAILED_) return BadRequest("The Program you're trying to add an application for does not exist");
            var result = await _applicationService.AddAsync(application);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Application application)
        {
            var result = await _applicationService.UpdateAsync(application.Id, application);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _applicationService.DeleteAsync(id);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }
    }
}