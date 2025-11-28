using NuGet.ContentModel;
using Smart_Library.SmartLibraryManagement.Service;
using Xunit;

namespace Smart_Library.UnitTest
{
    public class FacultyServiceTests
    {
        private readonly FacultyService _facultyService;

        public FacultyServiceTests()
        {
            _facultyService = new FacultyService();
        }

        [Theory]
        [InlineData("Teacher")]
        [InlineData("Dean")]
        [InlineData("Student")]
        public void GetFineCost_ReturnsCorrectValue(string position)
        {
            var facultyService = new FacultyService();
            decimal fine = facultyService.GetFineCost(position);

        }

        [Theory]
        [InlineData("Teacher")]
        [InlineData("Dean")]
        public void GetTotalFine_ReturnsCorrectTotal(string position)
        {
            var studentService = new StudentService();
            decimal fine = studentService.GetFineCost(position);
        }


        [Theory]
        [InlineData("Teacher", 5)]
        [InlineData("Dean", 9)]
        [InlineData("Unknown", 4)]
        public void GetDayDueDateByPosition_ReturnsCorrectDays(string position, int expectedDays)
        {
            int days = _facultyService.GetDayDueDateByPosition(position);
            Assert.Equal(expectedDays, days);
        }
    }

    public class StudentServiceTests
    {
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            _studentService = new StudentService();
        }

        //[Theory]
        //[InlineData("AnyPosition", 2, 4)]
        //[InlineData("AnyPosition", 3, 6)]
        //public void GetTotalFine_ReturnsCorrectTotal(string position, int numberOfData, float expectedTotal)
        //{
        //    decimal total = _studentService.GetTotalFine(numberOfData, position);
        //    Assert.Equal(expectedTotal, total);
        //}

        [Fact]
        public void GetDayDueDateByPosition_Returns3()
        {
            int days = _studentService.GetDayDueDateByPosition("AnyPosition");
            Assert.Equal(3, days);
        }
    }

    public class ValidationServiceTests
    {
        [Theory]
        [InlineData("Faculty", "Teacher", 5)]
        [InlineData("Faculty", "Dean", 9)]
        [InlineData("Student", "AnyPosition", 3)]
        public void GetDayDueDate_ReturnsCorrectDays(string role, string position, int expectedDays)
        {
            int days = ValidationService.GetDayDueDate(role, position);
            Assert.Equal(expectedDays, days);
        }
    }
}
