using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly DatabaseLibrary db;
        public BookController(DatabaseLibrary db)
        {
            this.db = db;
        }



        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = db.Books.ToList();
            return (!books.Any()) ? NotFound("No Books Registered") : Ok(books);
        }

        [HttpGet]
        [Route("/GetAllBorrowed")]
        public IActionResult GetAllBorrowedBook()
        {
            List <GetBooksAndRequest> borrowedbooks = new List<GetBooksAndRequest>();
            var get_loans = db.Loans.ToList();
            if (!get_loans.Any() || get_loans.Count() == 0)
                return NotFound("No Books Added");

            foreach (var loan in get_loans)
            {
                if (loan.book_id is null) continue;
                var get_loan = db.Loans.Find(loan.LoanId);
                foreach (var book in loan.book_id)
                {
                    var get_book = db.Books.Find(book);
                    if (get_book == null) continue;
                    var get_user = db.Users.Find(loan.ClientId);
                    var payload = new GetBooksAndRequest
                    {
                        book_id=get_book.BookId,
                        Title=get_book.Title,
                        Publisher=get_book.Publisher,
                        YearPublish=get_book.YearPublish,
                        BorrowDate=get_loan!.BorrowDate,
                        DueDate=get_loan!.DueDate,
                        Username=get_user!.Name,
                        Role=get_user!.Role,
                    };
                    borrowedbooks.Append(payload);
                }
            }
            return Json(borrowedbooks);
        }
    

        [HttpGet]
        [Route("{bookId:int}")]
        public IActionResult GetBook(int bookId)
        {
            var book = db.Books.Find(bookId);
            return (book == null) ? NotFound($"#404! Id {bookId} Not Found") : Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookDTO addBook)
        {
            var book = new Book()
            {
                ISBN = addBook.ISBN,
                Title = addBook.Title,
                Author = addBook.Author,
                Publisher = addBook.Publisher,
                YearPublish = addBook.YearPublish,
                Category = addBook.Category,
                isBorrowed = false,
                Condition = addBook.Condition,
                CreatedBy = addBook.CreatedBy,
                CreatedAt = addBook.CreatedAt
            };

            db.Books.Add(book);
            db.SaveChanges();

            var showResult = new GetBookDTO()
            {
                BookId = book.BookId,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                YearPublish = book.YearPublish,
                Category = book.Category,
                Condition = book.Condition,
                CreatedBy = book.CreatedBy,
                CreatedAt = book.CreatedAt,
                UpdatedBy = book.UpdatedBy,
                UpdatedAt = book.UpdatedAt
            };

            return Ok(showResult);
        }
        [HttpPut]
        [Route("BorrowBookUpdate/{bookId:int}")]
        public IActionResult updateIsBookBorrowed(int bookId, UpdateBookBorrowed databorrowed)
        {
            var getBook = db.Books.Find(bookId);
            if (getBook == null) return NotFound($"Book Not Found");

            getBook.isBorrowed =databorrowed.isBorrowed;
            db.Entry(getBook).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(db.Books.Find(bookId));
        }



        [HttpPut]
        [Route("{bookId:int}")]
        public IActionResult UpdateBook(int bookId, AddBookDTO updateBook)
        {
            var getBook = db.Books.Find(bookId);
            if (getBook == null) return NotFound($"#404, Id \"{bookId}\" Not Found");

            getBook.ISBN = updateBook.ISBN;
            getBook.Title = updateBook.Title;
            getBook.Author = updateBook.Author;
            getBook.Publisher = updateBook.Publisher;
            getBook.YearPublish = updateBook.YearPublish;
            getBook.Category = updateBook.Category;
            getBook.Condition = updateBook.Condition;
            getBook.CreatedBy = updateBook.CreatedBy;
            getBook.CreatedAt = updateBook.CreatedAt;

            db.Entry(getBook).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(db.Books.Find(bookId));
        }

        [HttpDelete]
        [Route("{bookId:int}")]
        public IActionResult DeleteBook(int bookId)
        {
            var getBook = db.Books.Find(bookId);
            if (getBook == null) return NotFound($"#404!, Id {bookId} Not Found");

            db.Books.Remove(getBook);
            db.SaveChanges();

            return Ok($"Book With Id {bookId} Deleted Successfully.");
        }
    }
}
