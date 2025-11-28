// <class>
using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.SmartLibraryManagement.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseLibrary _db;

        // <constructor>
        public BookRepository(DatabaseLibrary db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int bookId)
        {
            return await _db.Books.FindAsync(bookId);
        }

        public async Task AddAsync(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetBorrowedBooksAsync()
        {
            return await _db.Books.Where(b => b.isBorrowed == true).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBorrowedHistoryAsync()
        {
            var finishedLoans = await _db.Loans
                                         .Where(l => l.TransactionStatus == "Finished" && l.book_id != null)
                                         .ToListAsync();
            var books = new List<Book>();

            foreach (var loan in finishedLoans)
            {
                foreach (var bookId in loan.book_id!)
                {
                    var book = await _db.Books.FindAsync(bookId);
                    if (book != null) books.Add(book);
                }
            }
            return books;
        }
    }
}
