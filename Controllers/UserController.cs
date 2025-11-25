using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.DTOs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Smart_Library.SmartLibraryManagement.Service;

namespace Smart_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DatabaseLibrary db;
        public UserController(DatabaseLibrary db) 
        {
            this.db = db;  
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = db.Users.ToList();
            return (!users.Any() || users.Count() == 0) ? NotFound("No User Registered"): Ok(users);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetUser(int userId)
        {
            var user = db.Users.Find(userId);
            return (user == null) ? NotFound($"#404! ,Id {userId} Not Found") : Ok(user);
        }

        [HttpPost]
        [Route("/Register")]
        public IActionResult Register(AddUserDTOs addUser)
        {
            string pass_hash = BCrypt.Net.BCrypt.HashPassword(addUser.Password);
            string role = LoginService.GetRole(addUser.Email);
            var user = new User()
            {
                Name = addUser.Name,
                Email = addUser.Email,
                PasswordHash = pass_hash,
                isActive = true,
                Birthday = addUser.Birthday,
                Role = role,
            };
            db.Users.Add(user);
            db.SaveChanges();
            var showresult = new GetUserDTO()
            {
                UserId = user.UserId,
                Name = addUser.Name,
                Email = addUser.Email,
                isActive = addUser.isActive,
                Birthday = addUser.Birthday,
                Role = role,
            };
            return Ok(showresult);
        }

        [HttpPost]
        [Route("/LogIn")]
        public IActionResult Login(LoginDTO logindto)
        {
            var get_user = db.Users.Where(e => e.Email  == logindto.Email).FirstOrDefault();
            if (get_user == null) return Unauthorized("Invalid Credentials! Username Incorrect");
            if (BCrypt.Net.BCrypt.Verify(logindto.Password, get_user.PasswordHash) == false) return Unauthorized("Invalid Credentials! Password Incorrect");
            return Ok($"Login Successful, Welcome Back {get_user.Name}");
        }

        [HttpPut]
        [Route("{userId:int}")]
        public IActionResult UpdateUser(int userId, AddUserDTOs updateUser)
        {
            var get_user = db.Users.Find(userId);
            if (get_user == null) return NotFound($"#404, Id \"{userId}\" Not Found");
            get_user.Name = updateUser.Name;
            get_user.Birthday = updateUser.Birthday;
            db.Entry(get_user).State = EntityState.Modified;
            db.SaveChanges();
            var show_result = db.Users.Find(userId);
            return Ok(show_result);
        }

        [HttpDelete]
        [Route("{userId:int}")]
        public IActionResult DeleteStudyLoad(int userId)
        {
            var get_user = db.Users.Find(userId);
            if (get_user == null) return NotFound($"#404!, Id {userId} Not Found");
            db.Users.Remove(get_user);
            db.SaveChanges();
            return Ok($"User With Id {userId} Deleted Successfully.");
        }
    }
}
