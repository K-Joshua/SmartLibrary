using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.DTOs
{
    public class AddUserDTOs
    {
		public required string Name { get; set; }
        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required bool isActive { get; set; }
        public DateOnly? Birthday { get; set; }
        public required string Role { get; set; }
    }

    public class UpdateUserDTOs
    {
        public required string Username {get; set; }
    }

    public class GetUserDTO
    {
		public required string Name { get; set; }
        public required int UserId { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }

        public required bool isActive { get; set; }
        public DateOnly? Birthday { get; set; }
        public required string Role { get; set; }
    }

    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
