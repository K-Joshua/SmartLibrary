using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.Interface;

namespace Smart_Library.SmartLibraryManagement.Repository
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly DatabaseLibrary _db;

        public FacultyRepository(DatabaseLibrary db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Faculty>> GetAllAsync()
        {
            return await _db.Faculties.ToListAsync();
        }

        public async Task<Faculty?> GetByIdAsync(int facultyId)
        {
            return await _db.Faculties.FindAsync(facultyId);
        }

        public async Task AddAsync(Faculty faculty)
        {
            _db.Faculties.Add(faculty);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Faculty faculty)
        {
            _db.Entry(faculty).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Faculty faculty)
        {
            _db.Faculties.Remove(faculty);
            await _db.SaveChangesAsync();
        }
    }
}
