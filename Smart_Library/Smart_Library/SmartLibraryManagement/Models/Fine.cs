using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.Models
{
    public class Fine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FineId { get; set; }

        [Required]
        public required int LoanId { get; set; }       
        
        [Required]
        public decimal TotalAmount { get; set; }  
        
        public bool isPaid { get; set; }  = false;

        public DateTime? PaidAt { get; set; }          

        [Required]
        public required string CreatedBy { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

}
