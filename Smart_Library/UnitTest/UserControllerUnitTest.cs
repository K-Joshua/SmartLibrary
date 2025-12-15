using Microsoft.AspNetCore.Mvc;
using Moq;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Xunit;

namespace Smart_Library.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _repo;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _repo = new Mock<IUserRepository>();
            _controller = new UserController(_repo.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOk()
        {
            _repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User>
                {
                new User
                {
                    UserId = 1,
                    Name = "Kanade Yoisaki",
                    Username = "kanade",
                    Email = "kanade@nightcord.jp",
                    isActive = true,
                    Role = "Student",
                    PasswordHash = "hash"
                }
                });

            var result = await _controller.GetUsers();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUsers_ReturnsNotFound()
        {
            _repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User>());

            var result = await _controller.GetUsers();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetUser_ReturnsOk()
        {
            var user = new User
            {
                UserId = 1,
                Name = "Kanade Yoisaki",
                Username = "kanade",
                Email = "kanade@nightcord.jp",
                isActive = true,
                Role = "Student",
                PasswordHash = "hash"
            };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            var result = await _controller.GetUser(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound()
        {
            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User?)null);

            var result = await _controller.GetUser(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Register_ReturnsOk()
        {
            var dto = new AddUserDTOs
            {
                Name = "Kanade Yoisaki",
                Username = "kanade",
                Email = "kanade@nightcord.jp",
                Password = "password",
                isActive = true
            };

            _repo.Setup(r => r.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Register(dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsOk()
        {
            var password = "password";
            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                UserId = 1,
                Name = "Kanade Yoisaki",
                Username = "kanade",
                Email = "kanade@nightcord.jp",
                PasswordHash = hash,
                isActive = true,
                Role = "Student"
            };

            _repo.Setup(r => r.GetByEmailOrUsernameAsync("kanade@nightcord.jp"))
                .ReturnsAsync(user);

            var dto = new LoginDTO
            {
                Email = "kanade@nightcord.jp",
                Password = password
            };

            var result = await _controller.Login(dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            _repo.Setup(r => r.GetByEmailOrUsernameAsync("x@x.com"))
                .ReturnsAsync((User?)null);

            var dto = new LoginDTO
            {
                Email = "x@x.com",
                Password = "password"
            };

            var result = await _controller.Login(dto);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsOk()
        {
            var user = new User
            {
                UserId = 1,
                Name = "Kanade Yoisaki",
                Username = "old",
                Email = "kanade@nightcord.jp",
                isActive = true,
                Role = "Student",
                PasswordHash = "hash"
            };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _repo.Setup(r => r.UpdateAsync(user)).Returns(Task.CompletedTask);

            var dto = new UpdateUserDTOs
            {
                Username = "new"
            };

            var result = await _controller.UpdateUser(1, dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            var user = new User
            {
                UserId = 1,
                Name = "Kanade Yoisaki",
                Username = "kanade",
                Email = "kanade@nightcord.jp",
                isActive = true,
                Role = "Student",
                PasswordHash = "hash"
            };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _repo.Setup(r => r.DeleteAsync(user)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteUser(1);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}