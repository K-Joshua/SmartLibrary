using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Service;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : Controller
    {
        private readonly DatabaseLibrary db;
        public LoanController(DatabaseLibrary db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetLoans()
        {
            var loans = db.Loans.ToList();
            return (!loans.Any()) ? NotFound("No Loans Registered") : Ok(loans);
        }

        [HttpGet]
        [Route("{loanId:int}")]
        public IActionResult GetLoan(int loanId)
        {
            var loan = db.Loans.Find(loanId);
            return (loan == null) ? NotFound($"#404! Id {loanId} Not Found") : Ok(loan);
        }

        [HttpPost]
        public IActionResult AddLoan(AddLoanDTO addLoan)
        {
            if (addLoan.book_id is not null)
            {
                foreach (var book_ in addLoan.book_id)
                {
                    var check_books = db.Books.Find(book_);
                    if (check_books == null)
                        return NotFound($"Book Id {book_} Not Found");
                }
            }
            var loan = new Loan()
            {
                ClientId = addLoan.ClientId,
                TransactionType = addLoan.TransactionType,
                ReservedDate = addLoan.ReservedDate,
                BorrowDate = addLoan.BorrowDate,
                DueDate = addLoan.DueDate,
                ReturnDate = addLoan.ReturnDate,
                book_id = addLoan.book_id,
                TransactionStatus = addLoan.TransactionStatus,
                FineId = addLoan.FineId,
                CreatedBy = addLoan.CreatedBy,
                CreatedAt = addLoan.CreatedAt
            };

            db.Loans.Add(loan);
            db.SaveChanges();

            var showResult = new GetLoanDTO()
            {
                LoanId = loan.LoanId,
                ClientId = loan.ClientId,
                TransactionType = loan.TransactionType,
                ReservedDate = loan.ReservedDate,
                BorrowDate = loan.BorrowDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                TransactionStatus = loan.TransactionStatus,
                book_id = loan.book_id,
                FineId = loan.FineId,
                CreatedBy = loan.CreatedBy,
                CreatedAt = loan.CreatedAt,
                UpdatedBy = loan.UpdatedBy,
                UpdatedAt = loan.UpdatedAt
            };

            return Ok(showResult);
        }

        [HttpPut]
        [Route("/UpdateBorrowedBooks/{loanId:int}")]
        public IActionResult UpdateBorrowedBooks(UpdateBooksBorrowedInLoan borrowedbooks, int loanId)
        {
            var get_loan = db.Loans.Find(loanId);
            if (get_loan == null) 
                return NotFound("No Loan Found");
            if (borrowedbooks.book_id is not null)
            {
                foreach (var book_ in borrowedbooks.book_id)
                {
                    var check_books = db.Books.Find(book_);
                    if (check_books == null)
                        return NotFound($"Book Id {book_} Not Found");
                }
            }
            return Ok(borrowedbooks);
        }

        //[HttpPut]
        //[Route("/ClearBorrowedBooks/{loanId:int}")]
        //public IActionResult ClearBorrowedBooks(UpdateBooksBorrowedInLoan borrowedbooks, int loanId)
        //{

            //float fine;
            //var get_loan = db.Loans.Find(loanId);
            //if (get_loan == null) return NotFound("Loan Cannot Be Found");
            //var get_user = db.Users.Find(get_loan.ClientId);
            //if (get_user!.Role == "Student") fine = 2;
            //else;
            //if (get_loan.DueDate <= DateOnly.FromDateTime(DateTime.Now))
            //{

            //}
            //return Ok(borrowedbooks);
        //}

        [HttpPut]
        [Route("{loanId:int}")]
        public IActionResult UpdateLoan(int loanId, AddLoanDTO updateLoan)
        {
            var getLoan = db.Loans.Find(loanId);
            if (getLoan == null) return NotFound($"#404, Id \"{loanId}\" Not Found");

            getLoan.ClientId = updateLoan.ClientId;
            getLoan.TransactionType = updateLoan.TransactionType;
            getLoan.ReservedDate = updateLoan.ReservedDate;
            getLoan.BorrowDate = updateLoan.BorrowDate;
            getLoan.DueDate = updateLoan.DueDate;
            getLoan.ReturnDate = updateLoan.ReturnDate;
            getLoan.book_id = updateLoan.book_id;
            getLoan.TransactionStatus = updateLoan.TransactionStatus;
            getLoan.FineId = updateLoan.FineId;
            getLoan.CreatedBy = updateLoan.CreatedBy;
            getLoan.CreatedAt = updateLoan.CreatedAt;

            db.Entry(getLoan).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(db.Loans.Find(loanId));
        }

        [HttpDelete]
        [Route("{loanId:int}")]
        public IActionResult DeleteLoan(int loanId)
        {
            var getLoan = db.Loans.Find(loanId);
            if (getLoan == null) return NotFound($"#404!, Id {loanId} Not Found");

            db.Loans.Remove(getLoan);
            db.SaveChanges();

            return Ok($"Loan With Id {loanId} Deleted Successfully.");
        }
    }
}
