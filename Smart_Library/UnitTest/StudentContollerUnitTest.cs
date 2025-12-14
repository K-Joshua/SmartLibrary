using Microsoft.AspNetCore.Mvc;
using Moq;
using Smart_Library.Controllers;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Xunit;

namespace Smart_Library.Tests
{
    public class StudentControllerTests
    {
        private readonly Mock<IStudentRepository> _repo;
        private readonly StudentController _controller;

        public StudentControllerTests()
        {
            _repo = new Mock<IStudentRepository>();
            _controller = new StudentController(_repo.Object);
        }

        [Fact]
        public async Task GetBorrower_ReturnsOk()
        {
            _repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Student>
                {
                new Student
                {
                    StudentId = 1,
                    UserId = 1,
                    GradeLevel = 3,
                    Course = "Music Composition"
                }
                });

            var result = await _controller.GetBorrower();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBorrower_ReturnsNotFound()
        {
            _repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Student>());

            var result = await _controller.GetBorrower();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllUser_ReturnsOk()
        {
            _repo.Setup(r => r.GetAllWithUserAsync())
                .ReturnsAsync(new List<GetUserInformationStudent>
                {
                new GetUserInformationStudent
                {
                    StudentId = 1,
                    UserId = 1,
                    Name = "Kanade Yoisaki",
                    Username = "kanade",
                    Email = "kanade@nightcord.jp",
                    isActive = true,
                    Role = "Student",
                    GradeLevel = 3,
                    Course = "Music Composition"
                }
                });

            var result = await _controller.GetAllUser();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBorrowerById_ReturnsOk()
        {
            var student = new Student
            {
                StudentId = 1,
                UserId = 1,
                GradeLevel = 3,
                Course = "Music Composition"
            };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);

            var result = await _controller.GetBorrower(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBorrowerById_ReturnsNotFound()
        {
            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Student?)null);

            var result = await _controller.GetBorrower(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddBorrower_ReturnsOk()
        {
            var dto = new AddStudentDTO
            {
                UserId = 1,
                GradeLevel = 3,
                Course = "Music Composition"
            };

            _repo.Setup(r => r.AddAsync(It.IsAny<Student>()))
                 .Returns(Task.CompletedTask);

            var result = await _controller.AddBorrower(dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBorrower_ReturnsOk()
        {
            var student = new Student
            {
                StudentId = 1,
                UserId = 1,
                GradeLevel = 2,
                Course = "Old Course"
            };

            var dto = new AddStudentDTO
            {
                UserId = 1,
                GradeLevel = 3,
                Course = "Music Composition"
            };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);
            _repo.Setup(r => r.UpdateAsync(student)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateBorrower(1, dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBorrower_ReturnsOk()
        {
            var student = new Student
            {
                StudentId = 1,
                UserId = 1,
                GradeLevel = 3,
                Course = "Music Composition"
            };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);
            _repo.Setup(r => r.DeleteAsync(student)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteBorrower(1);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}