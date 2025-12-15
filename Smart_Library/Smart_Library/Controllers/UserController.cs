using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;
using Smart_Library.SmartLibraryManagement.Service;

[Route("SmartLibrary/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        if (!users.Any()) return NotFound("No User Registered");

        var payloadList = users.Select(u => new GetUserDTO
        {
            Name = u.Name,
            UserId = u.UserId,
            Username = u.Username,
            Email = u.Email,
            isActive = u.isActive,
            Role = u.Role
        }).ToList();

        return Ok(payloadList);
    }
        
    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return NotFound($"#404! Id {userId} Not Found");

        var payload = new GetUserDTO
        {
            Name = user.Name,
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            isActive = user.isActive,
            Role = user.Role
        };
        return Ok(payload);
    }

    [HttpPost("/Register")]
    public async Task<IActionResult> Register(AddUserDTOs addUser)
    {
        LoginService get_role = new LoginService();

        var pass_hash = BCrypt.Net.BCrypt.HashPassword(addUser.Password);
        var role = get_role.GetRole(addUser.Email);

        var user = new User
        {
            Name = addUser.Name,
            Username = addUser.Username,
            Email = addUser.Email,
            PasswordHash = pass_hash,
            isActive = true,
            Role = role
        };

        await _userRepository.AddAsync(user);

        var payload = new GetUserDTO
        {
            Name = user.Name,
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            isActive = user.isActive,
            Role = role
        };

        return Ok(payload);
    }

    [HttpPost("/LogIn")]
    public async Task<IActionResult> Login(LoginDTO logindto)
    {
        var user = await _userRepository.GetByEmailOrUsernameAsync(logindto.Email);
        if (user == null) return Unauthorized("Invalid Credentials! Email Incorrect");
        if (!BCrypt.Net.BCrypt.Verify(logindto.Password, user.PasswordHash))
            return Unauthorized("Invalid Credentials! Password Incorrect");

        return Ok(new { Message = $"Login Successful, Welcome Back {user.Username}" });
    }

    [HttpPut("{userId:int}")]
    public async Task<IActionResult> UpdateUser(int userId, UpdateUserDTOs updateUser)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return NotFound($"#404, Id \"{userId}\" Not Found");

        user.Username = updateUser.Username;
        await _userRepository.UpdateAsync(user);
            
        return Ok(user);
    }

    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return NotFound($"#404!, Id {userId} Not Found");

        await _userRepository.DeleteAsync(user);
        return Ok($"User With Id {userId} Deleted Successfully.");
    }
}
