using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using DotnetTaskAPI.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviewController : ControllerBase
    {
        private readonly IProgramService _programService;

        public PreviewController(IProgramService program)
        {
            _programService = program;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _programService.GetByIdAsync(id);
            if (result.Status == _Constants._FAILED_) return BadRequest(result);
            return Ok(result);
        }
    }
}