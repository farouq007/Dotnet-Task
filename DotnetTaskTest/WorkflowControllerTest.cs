using DotnetTaskAPI.Controllers;
using DotnetTaskAPI.Models;
using DotnetTaskAPI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetTaskTest
{
    public class WorkflowControllerTest
    {
        private readonly Mock<IWorkflowService> _mockWorkflowService;
        private readonly Mock<IProgramService> _mockProgramService;
        private readonly WorkflowController _controller;

        public WorkflowControllerTest()
        {
            _mockWorkflowService = new Mock<IWorkflowService>();
            _mockProgramService = new Mock<IProgramService>();
            _controller = new WorkflowController(_mockWorkflowService.Object, _mockProgramService.Object);
        }

        [Fact]
        public async Task GetById()
        {
            var validId = "b9a94ae-02e4-40b9-9cab-b03015efc832";
            var invalidId = "nxlsjk";

            //check for a wrong id
            var notOkResult = new GenericResponse { Status = _Constants._FAILED_ };
            _mockWorkflowService.Setup(x => x.GetByIdAsync(invalidId)).ReturnsAsync(notOkResult);
            var NotOkResponse = await _controller.Get(invalidId);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(NotOkResponse);
            Assert.Equal(notOkResult, badRequestResult.Value);

            // check for valid id
            var result = new GenericResponse { Status = _Constants._SUCCESS_, Data = new Workflow() };
            _mockWorkflowService.Setup(x => x.GetByIdAsync(validId)).ReturnsAsync(result);
            var response = await _controller.Get(validId);
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(result, okResult.Value);
        }
    }
}