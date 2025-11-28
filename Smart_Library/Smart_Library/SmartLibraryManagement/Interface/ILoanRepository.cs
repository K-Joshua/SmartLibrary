using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.SmartLibraryManagement.Interface
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsyncLoan(int loanId);
        Task<User?> GetByIdAsyncUser(int userid);
        Task<Faculty?> GetByIdAsyncFaculty(int facultyid);
        Task<Faculty?> GetFacultyByRole(int userid);
        Task UpdateBorrowedBookisBorrowed(List<int> bookId);
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task DeleteAsync(Loan loan);
    }
}
