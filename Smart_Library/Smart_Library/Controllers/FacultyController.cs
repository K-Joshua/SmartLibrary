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
    public class FacultyController : Controller, IBorrower
    {
        private readonly DatabaseLibrary db;
        public FacultyController(DatabaseLibrary db)
        {
            this.db = db;
        }

        [HttpGet]
		public IActionResult GetBorrower()
		{
            var faculties = db.Faculties.ToList();
            return (!faculties.Any()) ? NotFound("No Faculties Registered") : Ok(faculties);
        }

        [HttpGet]
        [Route("GetAllUser/Faculty")]
        public IActionResult GetAllUser()
        {
            List<GetUserInformationFaculty> payload = new List<GetUserInformationFaculty>();
            var get_faculty = db.Faculties.ToList();
            foreach (var faculty in get_faculty)
            {
                var get_user = db.Users.Find(faculty.UserId);
                var payloadOne = new GetUserInformationFaculty()
                {
                    UserId= faculty.UserId,
                    Name=get_user!.Name,
                    Email=get_user!.Email,
                    isActive=get_user!.isActive,
                    Role=get_user!.Role,
                    Username=get_user!.Username,
                    FacultytId=faculty.UserId,
                    Department=faculty.Department,
                    Position=faculty.Position,
                };
                payload.Add(payloadOne);
            }
            return Ok(payload);
        }

        [HttpGet]
		[Route("GetAllBookIssue/Faculty")]
		public IActionResult BookIssue()
		{
			List<BookIssueDto> borrowedbooks = new List<BookIssueDto>();
			var get_loans = db.Loans.Where(loan => loan.TransactionStatus == "Borrowed").ToList();
			if (!get_loans.Any() || get_loans.Count() == 0)
				return NotFound("No Books Added");
			foreach (var loan in get_loans)
			{
				if (loan.book_id is null) continue;
				var get_loan = db.Loans.Find(loan.LoanId);
				var get_user = db.Users.Find(loan.ClientId);
				if (get_user!.Role != "Faculty") continue;
				foreach (var book in loan.book_id)
				{
					var get_book = db.Books.Find(book);
					if (get_book == null) continue;
					var payload = new BookIssueDto
					{
						MemberId = get_user.UserId,
						BookId = get_book.BookId,
						LoanId = get_loan!.LoanId,
						Name = get_user.Name,
						Title = get_book.Title,
						Author = get_book.Author,
						BorrowDate = get_loan.BorrowDate,
						DueDate = get_loan.DueDate,
					};
					borrowedbooks.Add(payload);
				}
			}
			return Json(borrowedbooks);
		}

		[HttpGet]
        [Route("{id:int}")]
		public IActionResult GetBorrower(int id)
        {
            var faculty = db.Faculties.Find(id);
            return (faculty == null) ? NotFound($"#404! Id {id} Not Found") : Ok(faculty);
        }

        [HttpPost]
        public IActionResult AddFaculty(AddFacultyDTO addFaculty)
        {
            var get_user = db.Users.Find(addFaculty.UserId);
            if (get_user == null) return NotFound($"#404!, Id {addFaculty.UserId} Not Found");
            //get cost by role
            var faculty = new Faculty()
            {
                UserId = addFaculty.UserId,
                Department = addFaculty.Department,
                Position=addFaculty.Position,
            };

            db.Faculties.Add(faculty);
            db.SaveChanges();

            var showResult = new GetFacultyDTO()
            {
                FacultytId = faculty.FacultytId,
                UserId = faculty.UserId,
                Department = faculty.Department,
                Position = faculty.Position,
            };

            return Ok(showResult);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateFaculty(int id, UpdateFacultyDTO updateFaculty)
        {
            var getFaculty = db.Faculties.Find(id);
            if (getFaculty == null) return NotFound($"#404, Id \"{id}\" Not Found");

            var get_user = db.Users.Find(getFaculty.UserId);
            if (get_user == null) return NotFound($"#404!, Id {getFaculty.UserId} Not Found");

            getFaculty.Department = updateFaculty.Department;
            getFaculty.Position = updateFaculty.Position;
            db.Entry(getFaculty).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(db.Faculties.Find(id));
        }

        [HttpDelete]
        [Route("{id:int}")]
		public IActionResult DeleteBorrower(int id)
        {
            var getFaculty = db.Faculties.Find(id);
            if (getFaculty == null) return NotFound($"#404!, Id {id} Not Found");

            db.Faculties.Remove(getFaculty);
            db.SaveChanges();

            return Ok($"Faculty With Id {id} Deleted Successfully.");
        }
    }
}
