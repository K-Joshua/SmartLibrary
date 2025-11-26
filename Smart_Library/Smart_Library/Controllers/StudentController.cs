using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.Service;
using System.Linq;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller, IBorrower
	{
        private readonly DatabaseLibrary db;
        public StudentController(DatabaseLibrary db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetBorrower()
        {
            var students = db.Students.ToList();
            return (!students.Any()) ? NotFound("No Students Registered") : Ok(students);
        }

		[HttpGet]
		[Route("/GetAllBookIssue/")]
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
				if (get_user!.Role != "Student") continue;
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
            var student = db.Students.Find(id);
            return (student == null) ? NotFound($"#404! Id {id} Not Found") : Ok(student);
        }

        [HttpPost]
        public IActionResult AddBorrower(AddStudentDTO addStudent)
        {
            var get_user = db.Users.Find(addStudent.UserId);
            if (get_user == null) return NotFound($"#404!, Id {addStudent.UserId} Not Found");
            var student = new Student()
            {
                UserId = addStudent.UserId,
                GradeLevel = addStudent.GradeLevel,
                Course = addStudent.Course
            };
            db.Students.Add(student);
            db.SaveChanges();

            var showResult = new GetStudentDTO()
            {
                StudentId = student.StudentId,
                UserId = student.UserId,
                GradeLevel = student.GradeLevel,
                Course = student.Course
            };
            return Ok(showResult);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateBorrower(int id, AddStudentDTO updateStudent)
        {
            var getStudent = db.Students.Find(id);
            if (getStudent == null) return NotFound($"#404, Id \"{id}\" Not Found");
            var get_user = db.Users.Find(updateStudent.UserId);
            if (get_user == null) return NotFound($"#404!, Id {updateStudent.UserId} Not Found");
            getStudent.UserId = updateStudent.UserId;
            getStudent.GradeLevel = updateStudent.GradeLevel;
            getStudent.Course = updateStudent.Course;

            db.Entry(getStudent).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(db.Students.Find(id));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteBorrower(int id)
        {
            var getStudent = db.Students.Find(id);
            if (getStudent == null) return NotFound($"#404!, Id {id} Not Found");

            db.Students.Remove(getStudent);
            db.SaveChanges();

            return Ok($"Student With Id {id} Deleted Successfully.");
        }
    }
}
