using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.DTOs;
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
            if (!users.Any() || users.Count() == 0) return NotFound("No User Registered");
            List<GetUserDTO> payloadList = new List<GetUserDTO>();
            foreach(var user in users)
            {
                var payload = new GetUserDTO()
                {
                    Name=user.Name,
                    UserId=user.UserId,
                    Username=user.Username,
                    Email=user.Email,
                    isActive=user.isActive,
                    Role=user.Role
                };
                payloadList.Add(payload);
            }
            return Ok(payloadList);
        }

		[HttpGet]
		[Route("/GetUsername")]
		public IActionResult Getname(int id)
		{
			var user = db.Users.Find(id);
			return (user is null) ? NotFound("No Students Registered") : Ok(user.Name);
        }


		[HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetUser(int userId)
        {
            var user = db.Users.Find(userId);
            if (user is null) return NotFound($"#404! ,Id {userId} Not Found");
            var payload = new GetUserDTO()
            {
                Name=user.Name,
                UserId=user.UserId,
                Username=user.Username,
                Email=user.Email,
                isActive=user.isActive,
                Role=user.Role
            };
            return Ok(payload);
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
				Username = addUser.Username,
                Email = addUser.Email,
                PasswordHash = pass_hash,
                isActive = true,
                Role = role,
            };
            db.Users.Add(user);
            db.SaveChanges();
            var showresult = new GetUserDTO()
            {
                Name = addUser.Name,
                UserId = user.UserId,
				Username = addUser.Username,
                Email = addUser.Email,
                isActive = addUser.isActive,
                Role = role,
            };
            return Ok(showresult);
        }

        [HttpPost]
        [Route("/LogIn")]
        public IActionResult Login(LoginDTO logindto)
        {
            var get_user = db.Users.Where(e => e.Email  == logindto.Email || e.Username == logindto.Email).FirstOrDefault();
            if (get_user == null) return Unauthorized("Invalid Credentials! Email Incorrect");
            if (BCrypt.Net.BCrypt.Verify(logindto.Password, get_user.PasswordHash) == false) return Unauthorized("Invalid Credentials! Password Incorrect");
            return Ok(new { Message = $"Login Successful, Welcome Back {get_user.Username}" });
        }

        [HttpPut]
        [Route("{userId:int}")]
        public IActionResult UpdateUser(int userId, UpdateUserDTOs updateUser)
        {
            var get_user = db.Users.Find(userId);
            if (get_user == null) return NotFound($"#404, Id \"{userId}\" Not Found");
            get_user.Username = updateUser.Username;
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
