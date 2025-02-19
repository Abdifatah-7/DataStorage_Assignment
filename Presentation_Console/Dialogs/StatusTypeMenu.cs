using Business.Interfaces;
using Business.Models;

namespace PresentationConsoleApp.Dialogs;

public class StatusTypeMenu(IStatusTypeService statusTypeService)
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    public async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Status Types ===");
            Console.WriteLine("1. Create Status Type");
            Console.WriteLine("2. View All Status Types");
            Console.WriteLine("3. View Specific Status Type");
            Console.WriteLine("4. Update Status Type");
            Console.WriteLine("5. Delete Status Type");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateStatusType();
                    break;
                case "2":
                    await ViewAllStatusTypes();
                    break;
                case "3":
                    await ViewSpecificStatusType();
                    break;
                case "4":
                    await UpdateStatusType();
                    break;
                case "5":
                    await DeleteStatusType();
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

    public async Task CreateStatusType()
    {
        Console.Clear();
        Console.WriteLine("=== CREATE STATUS TYPE ===");

        Console.Write("Enter Status Type Name Example:\r\nnot started, ongoing, completed: ");
        var statusName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(statusName))
        {
            Console.WriteLine("\n Status Type name cannot be empty!");
            Console.ReadKey();
            return;
        }

        var registrationForm = new StatusTypeRegistration { StatusName = statusName };
        var result = await _statusTypeService.CreateStatusTypeAsync(registrationForm);

        Console.WriteLine(result ? "\n Status Type created successfully!" : "\n Status Type already exists.");
        Console.ReadKey();
    }

    public async Task ViewAllStatusTypes()
    {
        var statusTypes = await _statusTypeService.GetStatusTypesAsync();

        Console.Clear();
        Console.WriteLine("=== VIEW ALL STATUS TYPES ===");

        if (!statusTypes.Any())
        {
            Console.WriteLine("No status types found.");
        }
        else
        {
            foreach (var status in statusTypes)
            {
                Console.WriteLine($"ID: {status!.Id}, Name: {status.StatusName}");
            }
        }
        Console.ReadKey();
    }

    public async Task ViewSpecificStatusType()
    {
        Console.Clear();
        Console.WriteLine("=== VIEW SPECIFIC STATUS TYPE ===");

        Console.Write("Enter Status Type ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\n Invalid ID format!");
            Console.ReadKey();
            return;
        }

        var statusType = await _statusTypeService.GetStatusTypeByIdAsync(id);
        if (statusType == null)
            Console.WriteLine("\n Status Type not found!");
        else
            Console.WriteLine($" ID: {statusType.Id}, Name: {statusType.StatusName}");

        Console.ReadKey();
    }

    public async Task UpdateStatusType()
    {
        Console.Clear();
        Console.WriteLine("=== UPDATE STATUS TYPE ===");

        Console.Write("Enter Status Type ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\n Invalid ID format!");
            Console.ReadKey();
            return;
        }

        var existingStatus = await _statusTypeService.GetStatusTypeByIdAsync(id);
        if (existingStatus == null)
        {
            Console.WriteLine("\n Status Type not found!");
            Console.ReadKey();
            return;
        }

        Console.Write($"Enter new Status Type name (leave empty to keep '{existingStatus.StatusName}'): ");
        var newName = Console.ReadLine()?.Trim();
        newName = string.IsNullOrWhiteSpace(newName) ? existingStatus.StatusName : newName;

        var updatedStatusType = new StatusType
        {
            Id = id,
            StatusName = newName
        };

        var success = await _statusTypeService.UpdateStatusTypeAsync(updatedStatusType);
        Console.WriteLine(success ? "\n Status Type updated successfully!" : "\n Failed to update Status Type.");
        Console.ReadKey();
    }

    public async Task DeleteStatusType()
    {
        Console.Clear();
        Console.WriteLine("=== DELETE STATUS TYPE ===");

        Console.Write("Enter Status Type ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\n Invalid ID format!");
            Console.ReadKey();
            return;
        }

        var success = await _statusTypeService.DeleteStatusTypeAsync(id);
        Console.WriteLine(success ? "\n Status Type deleted successfully!" : "\n Failed to delete Status Type.");
        Console.ReadKey();
    }
}
