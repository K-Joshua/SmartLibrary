using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.DTOs
{
    public class AddFineDTO
    {
        public required int LoanId { get; set; }
        public required decimal TotalAmount { get; set; }
        public bool isPaid { get; set; }
        public DateTime? PaidAt { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
    }

    public class GetFineDTO
    {
        public required int FineId { get; set; }
        public required int LoanId { get; set; }
        public required decimal TotalAmount { get; set; }
        public bool isPaid { get; set; }
        public DateTime? PaidAt { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
