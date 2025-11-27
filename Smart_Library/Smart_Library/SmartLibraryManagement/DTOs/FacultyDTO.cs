using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.DTOs
{
    public class AddFacultyDTO
    {
        public required int UserId { get; set; }
        public required string Department { get; set; }
        public required string Position { get; set; }
    }

    public class UpdateFacultyDTO
    {
        public required string Department { get; set; }
        public required string Position { get; set; }
    }

    public class GetFacultyDTO
    {
        public required int FacultytId { get; set; }
        public required int UserId { get; set; }
        public required string Department { get; set; }
        public required string Position { get; set; }
    }

    public class GetUserInformationFaculty
    {
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required bool isActive { get; set; }
        public required string Role { get; set; }
        public required int FacultytId { get; set; }
        public required string Department { get; set; }
        public required string Position { get; set; }
    }
}
