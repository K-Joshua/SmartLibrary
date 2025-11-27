namespace Smart_Library.SmartLibraryManagement.Service
{
    public abstract class BaseService
    {
        public virtual Dictionary<string, float> finecost { get; } = new Dictionary<string, float>()
        {
        };

        public virtual Dictionary<string, int> daytime { get; } = new Dictionary<string, int>()
        {
        };
        public float GetFineCost(string position)
        {
            if (finecost.TryGetValue(position, out float fine))
                return fine;

            return 2; // or any default value, might be a student so 2
        }

        public virtual float GetTotalFine(int numberOfData, string position)
        {
            float fine = GetFineCost(position);
            float totalCost = (float)numberOfData * fine;
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
