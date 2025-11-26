namespace Smart_Library.SmartLibraryManagement.Service
{
    public class ValidationService
    {
        public static int GetDayDueDate(string role, string position)
        {
            FacultyService facultyService = new FacultyService();
            StudentService studentService = new StudentService();

            if (role == "Faculty")
            {
                return facultyService.GetDayDueDateByPosition(position);
            }
            else
            {
                return studentService.GetDayDueDateByPosition(position);
            }
        }
    }
}
