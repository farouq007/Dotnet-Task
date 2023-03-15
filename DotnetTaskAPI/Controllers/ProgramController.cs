using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _programService.GetAllAsync();
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _programService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProgramDetailsDTO program)
        {
            var result = await _programService.AddAsync(program);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] ProgramDetails program)
        {
            var result = await _programService.UpdateAsync(program.Id, program);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _programService.DeleteAsync(id);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }
    }
}