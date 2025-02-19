using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserRegistrationForm form);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User?>> GetUsersAsync();
        Task<bool> UpdateUserAsync(User user);
    }
}