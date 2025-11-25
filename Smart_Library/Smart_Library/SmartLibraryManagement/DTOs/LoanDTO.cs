using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.DTOs
{
    public class AddLoanDTO
    {
        public required int ClientId { get; set; }
        public required string TransactionType { get; set; } //borrow, buy, reserved
        public required DateOnly ReservedDate { get; set; }
        public required DateOnly BorrowDate { get; set; }
        public required DateOnly DueDate { get; set; }
        public required DateOnly ReturnDate { get; set; }
        public required List<int> book_id { get; set; }
        public required string TransactionStatus { get; set; }
        public required int FineId { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
    }

    public class UpdateBooksBorrowedInLoan()
    {
        public List<int> book_id { get; set; } = [];
    }

    public class GetLoanDTO
    {
        public required int LoanId { get; set; }
        public required int ClientId { get; set; }
        public required string TransactionType { get; set; }
        public required DateOnly ReservedDate { get; set; }
        public required DateOnly BorrowDate { get; set; }
        public required DateOnly DueDate { get; set; }
        public required DateOnly ReturnDate { get; set; }
        public List<int> book_id { get; set; } = [];
        public required string TransactionStatus { get; set; }
        public required int FineId { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
