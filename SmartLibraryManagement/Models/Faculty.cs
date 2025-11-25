using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.Models
{
    public class Faculty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacultytId { get; set; }

        [Required]
        public required int UserId { get; set; }

        [Required]
        public required string Department { get; set; }

        [Required]
        public required int GradeLevel { get; set; }

        [Required]
        public required string Position { get; set; }
    }
}
