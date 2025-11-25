namespace Smart_Library.SmartLibraryManagement.Service
{
    public class BorrowServiceBase
    {
        private Dictionary<string, float> finecost = new Dictionary<string, float>()
        {
            {"Student Teacher", 5},
            {"Teacher", 10},
            {"Instructor", 10},
            {"Senior Teacher", 15},
            {"Lecturer", 10},
            {"Head of Department", 20},
            {"HOD", 20},
            {"Dean", 25},
            {"Vice Dean", 30},
            {"Associate Dean", 30},
            {"Director", 35},
            {"Principal", 40}
        };
        public float GetFineCost(string position)
        {
            if (finecost.TryGetValue(position, out float fine))
                return fine;

            return 2; // or any default value, might be a student so 2
        }

        public virtual float GetTotalFine(int numberOfData, string position)
        {
            float finecost = GetFineCost(position);
            float totalCost = (float)numberOfData * finecost;
            return totalCost;
        }
    }
}
