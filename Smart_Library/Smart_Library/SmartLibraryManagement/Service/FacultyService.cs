using Microsoft.Identity.Client;

namespace Smart_Library.SmartLibraryManagement.Service
{
    public class FacultyService : BaseService 
    {
        public override Dictionary<string, float> finecost { get; } = new Dictionary<string, float>()
        {
            {"Student Teacher", 5},
            {"Teacher", 10},
            {"Instructor", 10},
            {"Senior Teacher", 15},
            {"Lecturer", 10},
            {"Head of Department", 20},
            {"HOD", 20},
            {"Dean", 30},
            {"Vice Dean", 25},
            {"Associate Dean", 30},
            {"Director", 35},
            {"Principal", 40}
        };

        public override Dictionary<string, int> daytime { get; } = new Dictionary<string, int>()
        {
            {"Student Teacher", 4},
            {"Teacher", 5},
            {"Instructor", 5},
            {"Senior Teacher", 6},
            {"Lecturer", 6},
            {"Head of Department", 7},
            {"HOD", 7},
            {"Dean", 9},
            {"Vice Dean", 8},
            {"Associate Dean", 9},
            {"Director", 10},
            {"Principal", 12}
        };
    }
}
