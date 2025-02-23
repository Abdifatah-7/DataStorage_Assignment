using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;


namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;


    public async Task<bool> CreateUserAsync(UserRegistrationForm form)
    {
        var existingUser = await _userRepository.GetAsync(u => u.Email == form.Email);
        if (existingUser != null)
            return false;

        var userEntity = UserFactory.Create(form);
        await _userRepository.CreateAsync(userEntity!);
        return true;
    }


    public async Task<IEnumerable<User?>> GetUsersAsync()
    {
        var userEntities = await _userRepository.GetAllAsync();
        return userEntities.Select(UserFactory.Create)!;
    }


    public async Task<User?> GetUserByIdAsync(int id)
    {
        var userEntity = await _userRepository.GetAsync(u => u.Id == id);
        return UserFactory.Create(userEntity!);
    }


    public async Task<bool> UpdateUserAsync(User user)
    {
        try
        {
            var existingUser = await _userRepository.GetAsync(u => u.Id == user.Id);
            if (existingUser == null)
                return false;

            UserFactory.UpdateEntity(existingUser, user);
            var result = await _userRepository.UpdateAsync(u => u.Id == user.Id, existingUser);
            return result != null;
        }
        catch (Exception)
        {
            return false;
        }
    }


    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            var userEntity = await _userRepository.GetAsync(u => u.Id == id);
            if (userEntity == null)
                return false;

            return await _userRepository.DeleteAsync(u => u.Id == id);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
