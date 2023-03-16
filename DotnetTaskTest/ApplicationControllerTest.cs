using AutoMapper;
using DotnetTaskAPI.Controllers;
using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using DotnetTaskAPI.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Moq;
using Xunit;

namespace DotnetTaskTest
{
    public class ApplicationControllerTest
    {
        private readonly Mock<IApplicationService> _mockApplicationService;
        private readonly Mock<IProgramService> _mockProgramService;
        private readonly ApplicationController _controller;

        public ApplicationControllerTest()
        {
            _mockApplicationService = new Mock<IApplicationService>();
            _mockProgramService = new Mock<IProgramService>();
            _controller = new ApplicationController(_mockApplicationService.Object, _mockProgramService.Object);
        }

        [Fact]
        public async Task GetById()
        {
            var validId = "b9a94ae-02e4-40b9-9cab-b03015efc832";
            var invalidId = "nxlsjk";

            //check for a wrong id
            var notOkResult = new GenericResponse { Status = _Constants._FAILED_ };
            _mockApplicationService.Setup(x => x.GetByIdAsync(invalidId)).ReturnsAsync(notOkResult);
            var NotOkResponse = await _controller.Get(invalidId);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(NotOkResponse);
            Assert.Equal(notOkResult, badRequestResult.Value);

            // check for valid id
            var result = new GenericResponse { Status = _Constants._SUCCESS_, Data = new Application() };
            _mockApplicationService.Setup(x => x.GetByIdAsync(validId)).ReturnsAsync(result);
            var response = await _controller.Get(validId);
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(result, okResult.Value);
        }
    }
}