using Business.Interfaces;
using Business.Models;

namespace PresentationConsoleApp.Dialogs;

public class UserMenu(IUserService userService)
{
    private readonly IUserService _userService = userService;

    public async Task Show()
    {
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Users ===");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. View All Users");
                Console.WriteLine("3. View Specific User");
                Console.WriteLine("4. Update User");
                Console.WriteLine("5. Delete User");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await CreateUser();
                        break;
                    case "2":
                        await ViewUsers();
                        break;
                    case "3":
                        await ViewSpecificUser();
                        break;
                    case "4":
                        await UpdateUser();
                        break;
                    case "5":
                        await DeleteUser();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    private async Task CreateUser()
    {
        Console.Clear();
        Console.WriteLine("=== CREATE A USER ===");

        Console.Write("Enter First Name: ");
        var firstName = Console.ReadLine()?.Trim();

        Console.Write("Enter Last Name: ");
        var lastName = Console.ReadLine()?.Trim();

        Console.Write("Enter Email: ");
        var email = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("\nAll fields are required!");
            Console.ReadKey();
            return;
        }

        var registrationForm = new UserRegistrationForm
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        var result = await _userService.CreateUserAsync(registrationForm);
        Console.WriteLine(result ? "\nUser created successfully!" : "\nFailed to create user.");
        Console.ReadKey();
    }




    public async Task ViewUsers()
    {
        Console.Clear();
        Console.WriteLine("=== View Users ===");

        var users = await _userService.GetUsersAsync();
        if (!users.Any())
        {
            Console.WriteLine("No users found.");
        }
        else
        {
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user!.Id}, Name: {user.FirstName} {user.LastName}, Email: {user.Email}");
            }
        }
        Console.ReadKey();
    }


    private async Task ViewSpecificUser()
    {
        Console.Clear();
        Console.WriteLine("=== View User ===");

        Console.Write("Enter User ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            Console.WriteLine("\nUser not found!");
        else
            Console.WriteLine($"ID: {user.Id}, Name: {user.FirstName} {user.LastName}, Email: {user.Email}");

        Console.ReadKey();
    }

    public async Task UpdateUser()
    {
        Console.Clear();
        Console.WriteLine("=== UPDATE USER ===");

        Console.Write("Enter User ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var existingUser = await _userService.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            Console.WriteLine("\nUser not found!");
            Console.ReadKey();
            return;
        }

        Console.Write($"Enter new first name (leave empty to keep '{existingUser.FirstName}'): ");
        var firstName = Console.ReadLine()?.Trim();
        firstName = string.IsNullOrWhiteSpace(firstName) ? existingUser.FirstName : firstName;

        Console.Write($"Enter new last name (leave empty to keep '{existingUser.LastName}'): ");
        var lastName = Console.ReadLine()?.Trim();
        lastName = string.IsNullOrWhiteSpace(lastName) ? existingUser.LastName : lastName;

        Console.Write($"Enter new email (leave empty to keep '{existingUser.Email}'): ");
        var email = Console.ReadLine()?.Trim();
        email = string.IsNullOrWhiteSpace(email) ? existingUser.Email : email;

        var updatedUser = new User
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        var success = await _userService.UpdateUserAsync(updatedUser);
        Console.WriteLine(success ? "\nUser updated successfully!" : "\nFailed to update user.");
        Console.ReadKey();
    }


    private async Task DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("=== DELETE USER ===");

        Console.Write("Enter User ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var success = await _userService.DeleteUserAsync(id);
        Console.WriteLine(success ? "\nUser deleted successfully!" : "\nFailed to delete user.");
        Console.ReadKey();
    }


}
