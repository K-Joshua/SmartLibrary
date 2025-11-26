using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(255)]
        [Required]
        public required string Username { get; set; }

		[MaxLength(255)]
		[Required]
		public required string Name { get; set; }

		[MaxLength(255)]
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public required bool isActive { get; set; }
        public DateOnly? Birthday { get; set; }
        [Required]
        public required string Role { get; set; }
    }
}
