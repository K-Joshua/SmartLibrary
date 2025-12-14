using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.Controllers
{
    [Route("SmartLibrary/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return (!books.Any()) ? NotFound("No Books Registered") : Ok(books);
        }

        [HttpGet("{bookId:int}")]
        public async Task<IActionResult> GetBook(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            return (book == null) ? NotFound($"#404! Id {bookId} Not Found") : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookDTO addBook)
        {
            var book = new Book
            {
                ISBN = addBook.ISBN,
                Title = addBook.Title,
                Author = addBook.Author,
                Publisher = addBook.Publisher,
                YearPublish = addBook.YearPublish,
                Category = addBook.Category,
                Condition = addBook.Condition,
                CreatedBy = "Librarian",
                CreatedAt = DateTime.UtcNow,
                isBorrowed = false
            };

            await _bookRepository.AddAsync(book);
            return Ok(book);
        }

        [HttpPut("{bookId:int}")]
        public async Task<IActionResult> UpdateBook(int bookId, AddBookDTO updateBook)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null) return NotFound($"#404! Id {bookId} Not Found");

            book.ISBN = updateBook.ISBN;
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Publisher = updateBook.Publisher;
            book.YearPublish = updateBook.YearPublish;
            book.Category = updateBook.Category;
            book.Condition = updateBook.Condition;
            book.UpdatedBy = "Librarian";
            book.UpdatedAt = DateTime.UtcNow;

            await _bookRepository.UpdateAsync(book);
            return Ok(book);
        }

        [HttpDelete("{bookId:int}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null) return NotFound($"#404! Id {bookId} Not Found");

            await _bookRepository.DeleteAsync(book);
            return Ok($"Book With Id {bookId} Deleted Successfully.");
        }

        [HttpGet("GetAllBorrowed")]
        public async Task<IActionResult> GetBorrowedBooks()
        {
            var borrowedBooks = await _bookRepository.GetBorrowedBooksAsync();
            return (!borrowedBooks.Any()) ? NotFound("No Borrowed Books") : Ok(borrowedBooks);
        }

        [HttpGet("GetAllBorrowed/History")]
        public async Task<IActionResult> GetBorrowedHistory()
        {
            var borrowedHistory = await _bookRepository.GetBorrowedHistoryAsync();
            return (!borrowedHistory.Any()) ? NotFound("No Borrowed Books History") : Ok(borrowedHistory);
        }
    }
}
