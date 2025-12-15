using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        public required string ISBN { get; set; }

        [MaxLength(255)]
        [Required]
        public required string Title { get; set; }

        [MaxLength(255)]
        [Required]
        public required string Author { get; set; }

        [MaxLength(255)]
        [Required]
        public required string Publisher { get; set; }

        [Required]
        public required DateOnly YearPublish { get; set; }

        [MaxLength(255)]
        [Required]
        public required string Category { get; set; }

        [MaxLength(255)]
        [Required]
        public required bool isBorrowed { get; set; } = false;

        [MaxLength(255)]
        [Required]
        public required string Condition { get; set; }

        [Required]
        public required string CreatedBy { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

	}
}
