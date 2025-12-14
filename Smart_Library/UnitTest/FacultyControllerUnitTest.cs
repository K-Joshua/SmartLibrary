using Microsoft.AspNetCore.Mvc;
using Moq;
using Smart_Library.Controllers;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Xunit;


namespace Smart_Library.Tests
{
    public class FacultyControllerTests
    {
        private readonly Mock<IFacultyRepository> _mockRepo;

        private readonly FacultyController _controller;

        public FacultyControllerTests()
        {
            _mockRepo = new Mock<IFacultyRepository>();
            _controller = new FacultyController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetFaculties_ReturnsOk_WhenFacultiesExist()
        {
            var faculties = new List<Faculty>
        {
            new Faculty
            {
                FacultytId = 1,
                UserId = 101,
                Department = "Music",
                Position = "Composer"
            }
        };

            _mockRepo.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(faculties);

            var result = await _controller.GetFaculties();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Faculty>>(okResult.Value);
            Assert.Single(value);
        }

        [Fact]
        public async Task GetFaculties_ReturnsNotFound_WhenEmpty()
        {
            _mockRepo.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(new List<Faculty>());

            var result = await _controller.GetFaculties();

            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task GetFaculty_ReturnsOk_WhenFound()
        {
            var faculty = new Faculty
            {
                FacultytId = 1,
                UserId = 101,
                Department = "Music",
                Position = "Composer"
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(faculty);

            var result = await _controller.GetFaculty(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(faculty, ok.Value);
        }

        [Fact]
        public async Task GetFaculty_ReturnsNotFound_WhenMissing()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync((Faculty?)null);

            var result = await _controller.GetFaculty(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddFaculty_ReturnsOk_WithFacultyDTO()
        {
            var dto = new AddFacultyDTO
            {
                UserId = 101,
                Department = "Music",
                Position = "Composer"
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Faculty>()))
                     .Returns(Task.CompletedTask);

            var result = await _controller.AddFaculty(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<GetFacultyDTO>(ok.Value);

            Assert.Equal(dto.UserId, value.UserId);
            Assert.Equal(dto.Department, value.Department);
            Assert.Equal(dto.Position, value.Position);
        }


        [Fact]
        public async Task UpdateFaculty_ReturnsOk_WhenExists()
        {
            var faculty = new Faculty
            {
                FacultytId = 1,
                UserId = 101,
                Department = "Music",
                Position = "Composer"
            };

            var dto = new UpdateFacultyDTO
            {
                Department = "Nightcord",
                Position = "Producer"
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(faculty);

            _mockRepo.Setup(r => r.UpdateAsync(faculty))
                     .Returns(Task.CompletedTask);

            var result = await _controller.UpdateFaculty(1, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var updated = Assert.IsType<Faculty>(ok.Value);

            Assert.Equal("Nightcord", updated.Department);
            Assert.Equal("Producer", updated.Position);
        }

        [Fact]
        public async Task UpdateFaculty_ReturnsNotFound_WhenMissing()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync((Faculty?)null);

            var dto = new UpdateFacultyDTO
            {
                Department = "Nightcord",
                Position = "Producer"
            };

            var result = await _controller.UpdateFaculty(1, dto);

            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task DeleteFaculty_ReturnsOk_WhenExists()
        {
            var faculty = new Faculty
            {
                FacultytId = 1,
                UserId = 101,
                Department = "Music",
                Position = "Composer"
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(faculty);

            _mockRepo.Setup(r => r.DeleteAsync(faculty))
                     .Returns(Task.CompletedTask);

            var result = await _controller.DeleteFaculty(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFaculty_ReturnsNotFound_WhenMissing()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync((Faculty?)null);

            var result = await _controller.DeleteFaculty(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }

}
