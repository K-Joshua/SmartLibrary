using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Smart_Library.SmartLibraryManagement.Service
{
    public class LoginService
    {
        public static string GetRole(string email)
        {
            string role;
            if (email.IndexOf("@Librarian", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                role = "Librarian";
            }
            else if (email.IndexOf("@Faculty", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                role = "Faculty";
            }
            else
            {
                role = "Student";
            }
            return role;
        }
    }

}