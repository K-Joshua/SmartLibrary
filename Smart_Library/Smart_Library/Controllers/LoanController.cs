using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.Repository;
using Smart_Library.SmartLibraryManagement.Service;

[Route("SmartLibrary/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILoanRepository _loanRepository;

    public LoanController(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetLoans()
    {
        var loans = await _loanRepository.GetAllAsync();
        return (!loans.Any()) ? NotFound("No Loans Registered") : Ok(loans);
    }

    [HttpGet("{loanId:int}")]
    public async Task<IActionResult> GetLoan(int loanId)
    {
        var loan = await _loanRepository.GetByIdAsyncLoan(loanId);
        return (loan == null) ? NotFound($"#404! Id {loanId} Not Found") : Ok(loan);
    }

    [HttpPost]
    public async Task<IActionResult> AddLoan(AddLoanDTO addLoan)
    {
        string faculty;
        if (addLoan.book_id == null || !addLoan.book_id.Any())
            return BadRequest("Cannot Add Loan Without Book To Borrow");

        var user = await _loanRepository.GetByIdAsyncUser(addLoan.ClientId);
        if (user == null) return NotFound("No User Found");

        await _loanRepository.UpdateBorrowedBookisBorrowed(addLoan.book_id);


        if (user.Role == "Faculty")
        {
            var get_faculty = await _loanRepository.GetFacultyByRole(user.UserId);
            faculty = get_faculty!.Position.ToString();
        }
        else faculty="Student";

        int get_day = ValidationService.GetDayDueDate(user.Role,faculty);
        DateOnly dueDate = addLoan.DueDate.AddDays(get_day);

        var loan = new Loan
        {
            ClientId = addLoan.ClientId,
            TransactionType = "Borrowed",
            ReservedDate = addLoan.ReservedDate,
            BorrowDate = addLoan.BorrowDate,
            DueDate = dueDate,
            ReturnDate = null,
            book_id = addLoan.book_id,
            TransactionStatus = "Borrowed",
            FineId = addLoan.FineId,
            CreatedBy = "Librarian",
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = "Librarian",
            UpdatedAt = DateTime.UtcNow
        };

        await _loanRepository.AddAsync(loan);

        return Ok(new GetLoanDTO
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
            UpdatedBy = "Librarian",
            UpdatedAt = DateTime.UtcNow
        });
    }

    [HttpPut("AddFine/{loanId:int}")]
    public async Task<IActionResult> UpdateBorrowedBooks(AddFineByLibrarian addfine, int loanId)
    {
        var loan = await _loanRepository.GetByIdAsyncLoan(loanId);
        if (loan == null) return NotFound("No Loan Found");

        loan.ReturnDate = addfine.ReturnDate;
        loan.FineId = addfine.FineId;
        loan.UpdatedAt = DateTime.UtcNow;
        loan.UpdatedBy = "Librarian";

        await _loanRepository.UpdateAsync(loan);
        return Ok(loan);
    }

 

    [HttpPut("UpdateBorrowedBooks/{loanId:int}")]
    public async Task<IActionResult> UpdateBorrowedBooks(UpdateBooksBorrowedInLoan borrowedbooks, int loanId)
    {
        var loan = await _loanRepository.GetByIdAsyncLoan(loanId);
        if (loan == null) return NotFound("No Loan Found");

        if (borrowedbooks.book_id is not null)
        {
            foreach (var bookId in borrowedbooks.book_id)
            {
                // Check if book exists, ideally via BookRepository
            }
        }

        loan.book_id = borrowedbooks.book_id;
        await _loanRepository.UpdateAsync(loan);
        return Ok(borrowedbooks);
    }

    [HttpDelete("{loanId:int}")]
    public async Task<IActionResult> DeleteLoan(int loanId)
    {
        var loan = await _loanRepository.GetByIdAsyncLoan(loanId);
        if (loan == null) return NotFound($"#404!, Id {loanId} Not Found");

        await _loanRepository.DeleteAsync(loan);
        return Ok($"Loan With Id {loanId} Deleted Successfully.");
    }
}
