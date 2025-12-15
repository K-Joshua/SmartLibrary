using Microsoft.AspNetCore.Mvc;
using Moq;
using Smart_Library.Controllers;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Xunit;

namespace Smart_Library.Tests
{
    public class FineControllerTests
    {
        private readonly Mock<IFineRepository> _repo;
        private readonly FineController _controller;

        public FineControllerTests()
        {
            _repo = new Mock<IFineRepository>();
            _controller = new FineController(_repo.Object);
        }

        [Fact]
        public async Task GetFines_ReturnsOk()
        {
            _repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Fine>
                {
                new Fine
                {
                    FineId = 1,
                    LoanId = 1,
                    TotalAmount = 100,
                    isPaid = false,
                    CreatedBy = "Librarian",
                    CreatedAt = DateTime.UtcNow
                }
                });

            var result = await _controller.GetFines();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFine_ReturnsOk()
        {
            var fine = new Fine
            {
                FineId = 1,
                LoanId = 1,
                TotalAmount = 50,
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Setup(r => r.GetByIdAsyncFine(1)).ReturnsAsync(fine);

            var result = await _controller.GetFine(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddFine_ReturnsOk()
        {
            var dto = new AddFineDTO
            {
                LoanId = 1,
                isPaid = false,
                PaidAt = null,
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            var loan = new Loan
            {
                LoanId = 1,
                ClientId = 1,
                TransactionType = "Borrow",
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                TransactionStatus = "Active",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow,
                book_id = new List<int> { 1 }
            };

            var user = new User
            {
                UserId = 1,
                Username = "kanade",
                Name = "Kanade Yoisaki",
                Email = "kanade@nightcord.jp",
                PasswordHash = "hashed",
                isActive = true,
                Role = "Student"
            };

            _repo.Setup(r => r.GetByIdAsyncLoan(1)).ReturnsAsync(loan);
            _repo.Setup(r => r.GetByIdAsyncUser(1)).ReturnsAsync(user);
            _repo.Setup(r => r.AddAsync(It.IsAny<Fine>())).Returns(Task.CompletedTask);

            var result = await _controller.AddFine(dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFine_ReturnsOk()
        {
            var fine = new Fine
            {
                FineId = 1,
                LoanId = 1,
                TotalAmount = 50,
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            var dto = new AddFineDTO
            {
                LoanId = 1,
                isPaid = true,
                PaidAt = DateTime.UtcNow,
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Setup(r => r.GetByIdAsyncFine(1)).ReturnsAsync(fine);
            _repo.Setup(r => r.UpdateAsync(fine)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateFine(1, dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFine_ReturnsOk()
        {
            var fine = new Fine
            {
                FineId = 1,
                LoanId = 1,
                TotalAmount = 50,
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Setup(r => r.GetByIdAsyncFine(1)).ReturnsAsync(fine);
            _repo.Setup(r => r.DeleteAsync(fine)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteFine(1);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}