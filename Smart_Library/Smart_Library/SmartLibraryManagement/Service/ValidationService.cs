namespace Smart_Library.SmartLibraryManagement.Service
{
    public abstract class ValidationService
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

        public static decimal GetFineByRoles(string role, int numberOfBooks,string position)
        {
            FacultyService facultyService = new FacultyService();
            StudentService studentService = new StudentService();

            if (role == "Faculty")
            {
                return facultyService.GetTotalFine(numberOfBooks, position);
            }
            else
            {
                return studentService.GetTotalFine(numberOfBooks, position);
            }
        }
    }
}
