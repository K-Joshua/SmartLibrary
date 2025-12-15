using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement.Interface;

namespace Smart_Library.SmartLibraryManagement.Service
{
    public class LoginService : ILogin
    {
        public string GetRole(string email)
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