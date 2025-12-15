using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smart_Library.Controllers;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
using System;

namespace Smart_Library.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<IBookRepository> _mockRepo;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mockRepo = new Mock<IBookRepository>();
            _controller = new BookController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult_WhenBooksExist()
        {
            var books = new List<Book>
            {
                new Book
                {
                    BookId = 1,
                    ISBN = "123",
                    Title = "Good Traits About Kanade",
                    Author = "Arsene",
                    Publisher = "TokyoColon",
                    YearPublish = DateOnly.FromDateTime(DateTime.UtcNow),
                    Category = "Autobiography",
                    Condition = "New",
                    CreatedBy = "Librarian",
                    CreatedAt = DateTime.UtcNow,
                    isBorrowed = false
                },
                new Book
                {
                    BookId = 2,
                    ISBN = "456",
                    Title = "Kanade's Favorite Songs",
                    Author = "Arsene",
                    Publisher = "TokyoColon",
                    YearPublish = DateOnly.FromDateTime(DateTime.UtcNow),
                    Category = "Music",
                    Condition = "New",
                    CreatedBy = "Librarian",
                    CreatedAt = DateTime.UtcNow,
                    isBorrowed = true
                }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            var result = await _controller.GetBooks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBooks = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(2, returnBooks.Count());
        }

        [Fact]
        public async Task GetBooks_ReturnsNotFound_WhenNoBooks()
        {
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Book>());

            var result = await _controller.GetBooks();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetBook_ReturnsOk_WhenBookExists()
        {
            var book = new Book
            {
                BookId = 1,
                ISBN = "123",
                Title = "Good Traits About Kanade",
                Author = "Arsene",
                Publisher = "TokyoColon",
                YearPublish = DateOnly.FromDateTime(DateTime.UtcNow),
                Category = "Autobiography",
                Condition = "New",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow,
                isBorrowed = false
            };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var result = await _controller.GetBook(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(book.BookId, returnBook.BookId);
        }

        [Fact]
        public async Task GetBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book?)null);

            var result = await _controller.GetBook(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddBook_ReturnsOk_WithCreatedBook()
        {
            var newBookDTO = new AddBookDTO
            {
                ISBN = "789",
                Title = "Kanade's Daily Routine",
                Author = "Arsene",
                Publisher = "TokyoColon",
                YearPublish = DateOnly.FromDateTime(DateTime.UtcNow),
                Category = "Diary",
                Condition = "New",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            var result = await _controller.AddBook(newBookDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var book = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(newBookDTO.Title, book.Title);
        }

        [Fact]
        public async Task UpdateBook_ReturnsOk_WhenBookExists()
        {
            var book = new Book
            {
                BookId = 1,
                ISBN = "123",
                Title = "Old Title About Kanade",
                Author = "Arsene",
                Publisher = "TokyoColon",
                YearPublish = DateOnly.FromDateTime(DateTime.UtcNow),
                Category = "Autobiography",
                Condition = "Used",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow,
                isBorrowed = false
            };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var updateDTO = new AddBookDTO
            {
                ISBN = "123",
                Title = "Updated Title About Kanade",
                Author = "Arsene",
                Publisher = "TokyoColon",
                YearPublish = DateOnly.FromDateTime(DateTime.UtcNow),
                Category = "Autobiography",
                Condition = "New",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow
            };

            var result = await _controller.UpdateBook(1, updateDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal("Updated Title About Kanade", updatedBook.Title);
        }

        [Fact]
        public async Task DeleteBook_ReturnsOk_WhenBookExists()
        {
            var book = new Book
            {
                BookId = 1,
                ISBN = "123",
                Title = "Good Traits About Kanade",
                Author = "Arsene",
                Publisher = "TokyoColon",
                YearPublish = DateOnly.FromDateTime(DateTime.UtcNow),
                Category = "Autobiography",
                Condition = "New",
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow,
                isBorrowed = false
            };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var result = await _controller.DeleteBook(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Book With Id 1 Deleted Successfully.", okResult.Value);
        }
    }
}
