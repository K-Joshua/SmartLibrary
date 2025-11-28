using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.DTOs
{
    public class GetBooksAndRequest
    {
        public int? book_id { get; set; }
        public string? Title { get; set; }
        public string? Publisher { get; set; }
        public DateOnly? YearPublish { get; set; }
        public DateOnly? BorrowDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
    }
    public class UpdateBookBorrowed
    {
        public required bool isBorrowed { get; set; } = false;
    }
    public class AddBookDTO
    {
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public required DateOnly YearPublish { get; set; }
        public required string Category { get; set; }
        public required string Condition { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
    }

    public class GetBookDTO
    {
        public required int BookId { get; set; }
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public required DateOnly YearPublish { get; set; }
        public required string Category { get; set; }
        public required bool isBorrowed { get; set; }
        public required string Condition { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class BookIssueDto
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public int LoanId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
		public required DateOnly BorrowDate { get; set; }
		public required DateOnly DueDate { get; set; }
	}
}
