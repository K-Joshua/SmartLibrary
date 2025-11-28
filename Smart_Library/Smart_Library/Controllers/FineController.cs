using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement.Service;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : Controller
    {
        private readonly DatabaseLibrary db;
        public FineController(DatabaseLibrary db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetFines()
        {
            var fines = db.Fines.ToList();
            return (!fines.Any()) ? NotFound("No Fines Registered") : Ok(fines);
        }

        [HttpGet]
        [Route("{fineId:int}")]
        public IActionResult GetFine(int fineId)
        {
            var fine = db.Fines.Find(fineId);
            return (fine == null) ? NotFound($"#404! Id {fineId} Not Found") : Ok(fine);
        }

        [HttpPost]
        public IActionResult AddFine(AddFineDTO addFine)
        {
            var get_loan = db.Loans.Find(addFine.LoanId);
            if (get_loan == null) return NotFound("No Loan Found");

            var get_user = db.Users.Find(get_loan.ClientId);
            string position;

            if (get_user!.Role == "Faculty")
            {
                var get_faculty = db.Faculties.Where(f => f.UserId == get_user.UserId).First();
                position = get_faculty.Position;
            } else position = "Student";

            if (get_loan.book_id is null) return NotFound("No Books To Fine Found");
            int get_book_count = get_loan.book_id.Count();

            decimal total_cost = ValidationService.GetFineByRoles(get_user.Role,get_book_count ,position);

            var fine = new Fine()
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
            db.Fines.Add(fine);
            db.SaveChanges();

            var showResult = new GetFineDTO()
            {
                FineId = fine.FineId,
                LoanId = fine.LoanId,
                TotalAmount = total_cost,
                isPaid = fine.isPaid,
                PaidAt = fine.PaidAt,
                CreatedBy = fine.CreatedBy,
                CreatedAt = fine.CreatedAt,
                UpdatedBy = fine.UpdatedBy,
                UpdatedAt = fine.UpdatedAt
            };

            return Ok(showResult);
        }

        [HttpPut]
        [Route("{fineId:int}")]
        public IActionResult UpdateFine(int fineId, AddFineDTO updateFine)
        {
            var getFine = db.Fines.Find(fineId);
            if (getFine == null) return NotFound($"#404, Id \"{fineId}\" Not Found");

            getFine.LoanId = updateFine.LoanId;
            getFine.isPaid = updateFine.isPaid;
            getFine.PaidAt = updateFine.PaidAt;
            getFine.CreatedBy = updateFine.CreatedBy;
            getFine.CreatedAt = updateFine.CreatedAt;

            db.Entry(getFine).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(db.Fines.Find(fineId));
        }

        [HttpDelete]
        [Route("{fineId:int}")]
        public IActionResult DeleteFine(int fineId) 
        {
            var getFine = db.Fines.Find(fineId);
            if (getFine == null) return NotFound($"#404!, Id {fineId} Not Found");

            db.Fines.Remove(getFine);
            db.SaveChanges();

            return Ok($"Fine With Id {fineId} Deleted Successfully.");
        }
    }
}
