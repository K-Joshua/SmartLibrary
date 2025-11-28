using Smart_Library.SmartLibraryManagement.Models;

namespace Smart_Library.SmartLibraryManagement.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
