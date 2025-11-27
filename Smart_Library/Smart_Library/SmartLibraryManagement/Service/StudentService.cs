namespace Smart_Library.SmartLibraryManagement.Service
{
    public class StudentService : BaseService
    {
        public override float GetTotalFine(int numberOfData, string position)
        {
            float totalCost = (float)numberOfData * 2;
            return totalCost;
        }

        public override int GetDayDueDateByPosition(string position)
        {
            return 3; // student 3 by default 
        }
    }
}
