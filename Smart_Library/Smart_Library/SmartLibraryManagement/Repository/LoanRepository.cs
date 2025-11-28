using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.SmartLibraryManagement.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DatabaseLibrary _db;

        public LoanRepository(DatabaseLibrary db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _db.Loans.OrderByDescending(k => k.LoanId).ToListAsync();
        }

        public async Task UpdateBorrowedBookisBorrowed(List<int> bookId)
        {
            foreach (int book in bookId)
            {
                var getBook = await _db.Books.FindAsync(book);
                getBook!.isBorrowed = true;
            }
            await _db.SaveChangesAsync();
        }
        public async Task<Loan?> GetByIdAsyncLoan(int loanId)
        {
            return await _db.Loans.FindAsync(loanId);
        }

        public async Task<User?> GetByIdAsyncUser(int userid)
        {
            return await _db.Users.FindAsync(userid);
        }
        public async Task<Faculty?> GetByIdAsyncFaculty(int facultyid)
        {
            return await _db.Faculties.FindAsync(facultyid);
        }

        public async Task<Faculty?> GetFacultyByRole(int userid)
        {

            return await _db.Faculties.FirstOrDefaultAsync(f => f.UserId == userid);
        }

        public async Task AddAsync(Loan loan)
        {
            _db.Loans.Add(loan);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Loan loan)
        {
            _db.Entry(loan).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Loan loan)
        {
            _db.Loans.Remove(loan);
            await _db.SaveChangesAsync();
        }
    }
}
