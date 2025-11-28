using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.SmartLibraryManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DatabaseLibrary _db;

        public StudentRepository(DatabaseLibrary db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _db.Students.OrderByDescending(s => s.StudentId).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _db.Students.FindAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _db.Entry(student).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetUserInformationStudent>> GetAllWithUserAsync()
        {
            List<GetUserInformationStudent> payload = new List<GetUserInformationStudent>();
            var get_student = await _db.Students.ToListAsync();
            foreach (var student in get_student)
            {
                var get_user = _db.Users.Find(student.UserId);
                var payloadOne = new GetUserInformationStudent()
                {
                    UserId= student.UserId,
                    StudentId=student.StudentId,
                    Name=get_user!.Name,
                    Email=get_user!.Email,
                    isActive=get_user!.isActive,
                    Role=get_user!.Role,
                    Username=get_user!.Username,
                    GradeLevel=student.GradeLevel,
                    Course=student.Course,
                };
                payload.Add(payloadOne);
            }
            return payload;
        }
    }
}
