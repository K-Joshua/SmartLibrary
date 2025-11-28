namespace Smart_Library.SmartLibraryManagement.Service
{
    public class StudentService : BaseService
    {
        public override decimal GetTotalFine(int numberOfData, string position)
        {
            decimal totalCost = (decimal)numberOfData * 2;
            return totalCost;
        }

        public override int GetDayDueDateByPosition(string position)
        {
            return 3; // student 3 by default 
        }
    }
}
