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
    public class ProgramControllerTest
    {
        private readonly Mock<IProgramService> _mockProgramService;
        private readonly ProgramController _controller;

        public ProgramControllerTest()
        {
            _mockProgramService = new Mock<IProgramService>();
            _controller = new ProgramController(_mockProgramService.Object);
        }

        [Fact]
        public async Task GetById()
        {
            var validId = "b9a94ae-02e4-40b9-9cab-b03015efc832";
            var invalidId = "nxlsjk";

            //check for a wrong id
            var notOkResult = new GenericResponse { Status = _Constants._FAILED_ };
            _mockProgramService.Setup(x => x.GetByIdAsync(invalidId)).ReturnsAsync(notOkResult);
            var NotOkResponse = await _controller.Get(invalidId);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(NotOkResponse);
            Assert.Equal(notOkResult, badRequestResult.Value);

            // check for valid id
            var result = new GenericResponse { Status = _Constants._SUCCESS_, Data = new Application() };
            _mockProgramService.Setup(x => x.GetByIdAsync(validId)).ReturnsAsync(result);
            var response = await _controller.Get(validId);
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var programDetails = new ProgramDetails
            {
                Id = "string",
                Title = "Test Program",
                Description = "Hello",
                KeySkills = "C#",
                ProgramType = "Fulltime",
                ApplicationEndDate = DateTime.Now,
                ApplicationStartDate = DateTime.Now,
            };
            var expected = new GenericResponse { Status = _Constants._SUCCESS_ };
            _mockProgramService.Setup(x => x.UpdateAsync(programDetails.Id, programDetails)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Edit(programDetails);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var id = "string";
            var expected = new GenericResponse { Status = _Constants._SUCCESS_ };
            _mockProgramService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }
    }
}