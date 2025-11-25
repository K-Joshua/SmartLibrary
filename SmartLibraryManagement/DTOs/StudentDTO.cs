using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library.SmartLibraryManagement.DTOs
{
    public class AddStudentDTO
    {
        public required int UserId { get; set; }
        public required int GradeLevel { get; set; }
        //public required int TotalNumberOfBooksBorrowed { get; set; }
        //public required int CurrentNumberOfBooksBorrowed { get; set; }
        public required string Course { get; set; }
    }

    public class GetStudentDTO
    {
        public required int StudentId { get; set; }
        public required int UserId { get; set; }
        public required int GradeLevel { get; set; }
        //public required int TotalNumberOfBooksBorrowed { get; set; }
        //public required int CurrentNumberOfBooksBorrowed { get; set; }
        public required string Course { get; set; }
    }
}
