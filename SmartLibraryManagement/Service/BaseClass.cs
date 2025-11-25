namespace Smart_Library.SmartLibraryManagement.Service
{
    public class BaseService
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
        public virtual float GetFineCost(string role)
        {
            if (finecost.TryGetValue(role, out float fine))
                return fine;

            return 5; // or any default value, might be a student so 2
        }
    }
}
