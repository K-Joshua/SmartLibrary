using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly DatabaseLibrary db;
        public StudentController(DatabaseLibrary db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = db.Students.ToList();
            return (!students.Any()) ? NotFound("No Students Registered") : Ok(students);
        }

        [HttpGet]
        [Route("{studentId:int}")]
        public IActionResult GetStudent(int studentId)
        {
            var student = db.Students.Find(studentId);
            return (student == null) ? NotFound($"#404! Id {studentId} Not Found") : Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentDTO addStudent)
        {
            var get_user = db.Users.Find(addStudent.UserId);
            if (get_user == null) return NotFound($"#404!, Id {addStudent.UserId} Not Found");
            var student = new Student()
            {
                UserId = addStudent.UserId,
                GradeLevel = addStudent.GradeLevel,
                //TotalNumberOfBooksBorrowed = addStudent.TotalNumberOfBooksBorrowed,
                //CurrentNumberOfBooksBorrowed = addStudent.CurrentNumberOfBooksBorrowed,
                Course = addStudent.Course
            };
            db.Students.Add(student);
            db.SaveChanges();

            var showResult = new GetStudentDTO()
            {
                StudentId = student.StudentId,
                UserId = student.UserId,
                GradeLevel = student.GradeLevel,
                //TotalNumberOfBooksBorrowed = student.TotalNumberOfBooksBorrowed,
                //CurrentNumberOfBooksBorrowed = student.CurrentNumberOfBooksBorrowed,
                Course = student.Course
            };
            return Ok(showResult);
        }

        [HttpPut]
        [Route("{studentId:int}")]
        public IActionResult UpdateStudent(int studentId, AddStudentDTO updateStudent)
        {
            var getStudent = db.Students.Find(studentId);
            if (getStudent == null) return NotFound($"#404, Id \"{studentId}\" Not Found");
            var get_user = db.Users.Find(updateStudent.UserId);
            if (get_user == null) return NotFound($"#404!, Id {updateStudent.UserId} Not Found");
            getStudent.UserId = updateStudent.UserId;
            getStudent.GradeLevel = updateStudent.GradeLevel;
            //getStudent.TotalNumberOfBooksBorrowed = updateStudent.TotalNumberOfBooksBorrowed;
            //getStudent.CurrentNumberOfBooksBorrowed = updateStudent.CurrentNumberOfBooksBorrowed;
            getStudent.Course = updateStudent.Course;

            db.Entry(getStudent).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(db.Students.Find(studentId));
        }

        [HttpDelete]
        [Route("{studentId:int}")]
        public IActionResult DeleteStudent(int studentId)
        {
            var getStudent = db.Students.Find(studentId);
            if (getStudent == null) return NotFound($"#404!, Id {studentId} Not Found");

            db.Students.Remove(getStudent);
            db.SaveChanges();

            return Ok($"Student With Id {studentId} Deleted Successfully.");
        }
    }
}
