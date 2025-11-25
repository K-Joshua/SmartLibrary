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
            
            var fine = new Fine()
            {
                LoanId = addFine.LoanId,
                TotalAmount = addFine.TotalAmount,
                isPaid = addFine.isPaid,
                PaidAt = addFine.PaidAt,
                CreatedBy = addFine.CreatedBy,
                CreatedAt = addFine.CreatedAt
            };

            db.Fines.Add(fine);
            db.SaveChanges();

            var showResult = new GetFineDTO()
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
            getFine.TotalAmount = updateFine.TotalAmount;
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
