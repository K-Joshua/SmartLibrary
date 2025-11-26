using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.SmartLibraryManagement
{
    public class DatabaseLibrary : DbContext
    {
        public DatabaseLibrary(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Loan>()
                .Property(Loan => Loan.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Loan>()
                .Property(Loan => Loan.BorrowDate)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Loan>()
                .Property(Loan => Loan.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Fine>()
                .Property(Fine => Fine.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.00);

            modelBuilder.Entity<Fine>()
                .Property(Fine => Fine.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Fine>()
                .Property(Fine => Fine.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Book>()
                .Property(Book => Book.Condition)
                .HasDefaultValue("Good");

            modelBuilder.Entity<Book>()
                .Property(Book => Book.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Book>()
                .Property(Book => Book.UpdatedAt)
                .HasDefaultValueSql("NOW()");

        }
    }
}
