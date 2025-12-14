using Microsoft.AspNetCore.Mvc;
using Moq;
using Smart_Library.Controllers;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Xunit;

namespace Smart_Library.Tests
{
    public class LoanControllerTests
    {
        private readonly Mock<ILoanRepository> _repo;
        private readonly LoanController _controller;

        public LoanControllerTests()
        {
            _repo = new Mock<ILoanRepository>();
            _controller = new LoanController(_repo.Object);
        }

        [Fact]
        public async Task GetLoans_ReturnsOk()
        {
            _repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Loan>
                {
                new Loan
                {
                    LoanId = 1,
                    ClientId = 1,
                    TransactionType = "Borrowed",
                    BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                    TransactionStatus = "Borrowed",
                    CreatedBy = "Librarian",
                    CreatedAt = DateTime.UtcNow
                }
                });

            var result = await _controller.GetLoans();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetLoan_ReturnsOk()
        {
            var loan = new Loan
            {
                LoanId = 1,
                ClientId = 1,
                TransactionType = "Borrowed",
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                TransactionStatus = "Borrowed",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Setup(r => r.GetByIdAsyncLoan(1)).ReturnsAsync(loan);

            var result = await _controller.GetLoan(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddLoan_ReturnsBadRequest_WhenNoBooks()
        {
            var dto = new AddLoanDTO
            {
                ClientId = 1,
                TransactionType = "Borrowed",
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow),
                book_id = new List<int>(),
                TransactionStatus = "Borrowed",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            var result = await _controller.AddLoan(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddLoan_ReturnsOk()
        {
            var dto = new AddLoanDTO
            {
                ClientId = 1,
                TransactionType = "Borrowed",
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow),
                book_id = new List<int> { 1 },
                TransactionStatus = "Borrowed",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
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

            _repo.Setup(r => r.GetByIdAsyncUser(1)).ReturnsAsync(user);
            _repo.Setup(r => r.UpdateBorrowedBookisBorrowed(dto.book_id))
                 .Returns(Task.CompletedTask);
            _repo.Setup(r => r.AddAsync(It.IsAny<Loan>()))
                 .Returns(Task.CompletedTask);

            var result = await _controller.AddLoan(dto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFine_ReturnsOk()
        {
            var loan = new Loan
            {
                LoanId = 1,
                ClientId = 1,
                TransactionType = "Borrowed",
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                TransactionStatus = "Borrowed",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            var dto = new AddFineByLibrarian
            {
                FineId = 10,
                ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _repo.Setup(r => r.GetByIdAsyncLoan(1)).ReturnsAsync(loan);
            _repo.Setup(r => r.UpdateAsync(loan)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateBorrowedBooks(dto, 1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteLoan_ReturnsOk()
        {
            var loan = new Loan
            {
                LoanId = 1,
                ClientId = 1,
                TransactionType = "Borrowed",
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                TransactionStatus = "Borrowed",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Setup(r => r.GetByIdAsyncLoan(1)).ReturnsAsync(loan);
            _repo.Setup(r => r.DeleteAsync(loan)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteLoan(1);

            Assert.IsType<OkObjectResult>(result);
        }
    }

}