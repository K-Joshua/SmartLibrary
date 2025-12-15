using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.SmartLibraryManagement.Repository
{
    public class FineRepository : IFineRepository
    {
        private readonly DatabaseLibrary _db;

        public FineRepository(DatabaseLibrary db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Fine>> GetAllAsync()
        {
            return await _db.Fines.ToListAsync();
        }

        public async Task<Fine?> GetByIdAsyncFine(int fineId)
        {
            return await _db.Fines.FindAsync(fineId);
        }
        public async Task<User?> GetByIdAsyncUser(int userId)
        {
            return await _db.Users.FindAsync(userId);
        }


        public async Task<Loan?> GetByIdAsyncLoan(int loanId)
        {
            return await _db.Loans.FindAsync(loanId);
        }

        public async Task AddAsync(Fine fine)
        {
            _db.Fines.Add(fine);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Fine fine)
        {
            _db.Entry(fine).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Fine fine)
        {
            _db.Fines.Remove(fine);
            await _db.SaveChangesAsync();
        }
    }
}
