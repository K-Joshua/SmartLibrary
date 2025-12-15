using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int LoanId { get; set; }

        [Required]
        public required int ClientId { get; set; }

        [MaxLength(255)]
        [Required]
        public required string TransactionType { get; set; }     // Borrow, Damage, Lost

        public DateOnly? ReservedDate { get; set; }

        [Required]
        public required DateOnly BorrowDate { get; set; }

        [Required]
        public required DateOnly DueDate { get; set; }

        public DateOnly? ReturnDate { get; set; }

        public List<int>? book_id { get; set; } = [];

        [Required]
        public required string TransactionStatus { get; set; }

        public int? FineId { get; set; }

        [Required]
        public required string CreatedBy { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
