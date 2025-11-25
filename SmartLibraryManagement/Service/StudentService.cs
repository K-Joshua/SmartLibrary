namespace Smart_Library.SmartLibraryManagement.Service
{
    public class StudentService : BorrowServiceBase
    {
        public override float GetTotalFine(int numberOfData, string position)
        {
            float totalCost = (float)numberOfData * 2;
            return totalCost;
        }
    }
}
