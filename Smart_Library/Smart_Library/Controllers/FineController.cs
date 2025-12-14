using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.Service;

[Route("SmartLibrary/[controller]")]
[ApiController]
public class FineController : ControllerBase
{
    private readonly IFineRepository _fineRepository;

    public FineController(IFineRepository fineRepository)
    {
        _fineRepository = fineRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetFines()
    {
        var fines = await _fineRepository.GetAllAsync();
        return (!fines.Any()) ? NotFound("No Fines Registered") : Ok(fines);
    }

    [HttpGet("{fineId:int}")]
    public async Task<IActionResult> GetFine(int fineId)
    {
        var fine = await _fineRepository.GetByIdAsyncFine(fineId);
        return (fine == null) ? NotFound($"#404! Id {fineId} Not Found") : Ok(fine);
    }

    [HttpPost]
    public async Task<IActionResult> AddFine(AddFineDTO addFine)
    {
        var loan = await _fineRepository.GetByIdAsyncLoan(addFine.LoanId);
        if (loan == null) return NotFound("No Loan Found");

        var user = await _fineRepository.GetByIdAsyncUser(loan.ClientId);
        string position = (user!.Role == "Faculty") ? "FacultyPosition" : "Student";

        if (loan.book_id == null) return NotFound("No Books To Fine Found");

        decimal total_cost = ValidationService.GetFineByRoles(user.Role, loan.book_id.Count, position);

        var fine = new Fine
        {
            LoanId = addFine.LoanId,
            TotalAmount = total_cost,
            isPaid = addFine.isPaid,
            PaidAt = addFine.PaidAt,
            CreatedBy = "Librarian",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = "Librarian"
        };

        await _fineRepository.AddAsync(fine);

        return Ok(new GetFineDTO
        {
            FineId = fine.FineId,
            LoanId = fine.LoanId,
            TotalAmount = fine.TotalAmount,
            isPaid = fine.isPaid,
            PaidAt = fine.PaidAt,
            CreatedBy = fine.CreatedBy,
            CreatedAt = fine.CreatedAt,
            UpdatedBy = fine.UpdatedBy,
            UpdatedAt = fine.UpdatedAt
        });
    }

    [HttpPut("{fineId:int}")]
    public async Task<IActionResult> UpdateFine(int fineId, AddFineDTO updateFine)
    {
        var fine = await _fineRepository.GetByIdAsyncFine(fineId);
        if (fine == null) return NotFound($"#404, Id {fineId} Not Found");

        fine.LoanId = updateFine.LoanId;
        fine.isPaid = updateFine.isPaid;
        fine.PaidAt = updateFine.PaidAt;
        fine.CreatedBy = updateFine.CreatedBy;
        fine.CreatedAt = updateFine.CreatedAt;

        await _fineRepository.UpdateAsync(fine);
        return Ok(fine);
    }

    [HttpDelete("{fineId:int}")]
    public async Task<IActionResult> DeleteFine(int fineId)
    {
        var fine = await _fineRepository.GetByIdAsyncFine(fineId);
        if (fine == null) return NotFound($"#404!, Id {fineId} Not Found");

        await _fineRepository.DeleteAsync(fine);
        return Ok($"Fine With Id {fineId} Deleted Successfully.");
    }
}
