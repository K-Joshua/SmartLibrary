using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : Controller
    {
        private readonly DatabaseLibrary db;
        public FacultyController(DatabaseLibrary db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetFaculties()
        {
            var faculties = db.Faculties.ToList();
            return (!faculties.Any()) ? NotFound("No Faculties Registered") : Ok(faculties);
        }

        [HttpGet]
        [Route("{facultyId:int}")]
        public IActionResult GetFaculty(int facultyId)
        {
            var faculty = db.Faculties.Find(facultyId);
            return (faculty == null) ? NotFound($"#404! Id {facultyId} Not Found") : Ok(faculty);
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
                GradeLevel = addFaculty.GradeLevel,
                Position=addFaculty.Position,
            };

            db.Faculties.Add(faculty);
            db.SaveChanges();

            var showResult = new GetFacultyDTO()
            {
                FacultytId = faculty.FacultytId,
                UserId = faculty.UserId,
                Department = faculty.Department,
                GradeLevel = faculty.GradeLevel,
                Position = faculty.Position,
            };

            return Ok(showResult);
        }

        [HttpPut]
        [Route("{facultyId:int}")]
        public IActionResult UpdateFaculty(int facultyId, AddFacultyDTO updateFaculty)
        {
            var get_user = db.Users.Find(updateFaculty.UserId);
            if (get_user == null) return NotFound($"#404!, Id {updateFaculty.UserId} Not Found");
            var getFaculty = db.Faculties.Find(facultyId);
            if (getFaculty == null) return NotFound($"#404, Id \"{facultyId}\" Not Found");

            getFaculty.UserId = updateFaculty.UserId;
            getFaculty.Department = updateFaculty.Department;
            getFaculty.GradeLevel = updateFaculty.GradeLevel;
            getFaculty.Position = updateFaculty.Position;
            db.Entry(getFaculty).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(db.Faculties.Find(facultyId));
        }

        [HttpDelete]
        [Route("{facultyId:int}")]
        public IActionResult DeleteFaculty(int facultyId)
        {
            var getFaculty = db.Faculties.Find(facultyId);
            if (getFaculty == null) return NotFound($"#404!, Id {facultyId} Not Found");

            db.Faculties.Remove(getFaculty);
            db.SaveChanges();

            return Ok($"Faculty With Id {facultyId} Deleted Successfully.");
        }
    }
}
