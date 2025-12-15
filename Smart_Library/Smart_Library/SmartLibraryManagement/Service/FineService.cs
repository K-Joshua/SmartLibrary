namespace Smart_Library.SmartLibraryManagement.Service
{
    public class FineService
    {
        public virtual Dictionary<string, decimal> finecost { get; } = new Dictionary<string, decimal>()
        {
        };

        public virtual Dictionary<string, int> daytime { get; } = new Dictionary<string, int>()
        {
        };
        public decimal GetFineCost(string position)
        {
            if (finecost.TryGetValue(position, out decimal fine))
                return fine;

            return 2; // or any default value, might be a student so 2
        }

        public virtual decimal GetTotalFine(int numberOfData, string position)
        {
            decimal fine = GetFineCost(position);
            decimal totalCost = (decimal)numberOfData * fine;
            return totalCost;
        }
        public virtual int GetDayDueDateByPosition(string position)
        {
            if (daytime.TryGetValue(position, out int day))
                return day;

            return 4; // or any default value, might be a student teacher so 4
        }
    }
}
