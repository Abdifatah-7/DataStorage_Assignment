
using Business.Interfaces;
using Business.Models;


namespace PresentationConsoleApp.Dialogs;

public class ProjectMenu(IProjectService projectService)
{
    private readonly IProjectService _projectService = projectService;

    public async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Projects ===");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. View All Projects");
            Console.WriteLine("3. View Specific Project");
            Console.WriteLine("4. Update Project");
            Console.WriteLine("5. Delete Project");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Choose an option: ");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    await CreateProject();
                    break;
                case "2":
                    await ViewProjects();
                    break;
                case "3":
                    await UpdateProject();
                    break;
                case "4":
                    await DeleteProject();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public async Task CreateProject()
    {
        Console.Clear();
        Console.WriteLine("=== CREATE A PROJECT ===");

        Console.Write("Enter Project Name: ");
        var projectName = Console.ReadLine()?.Trim();

        Console.Write("Enter Total Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal totalPrice))
        {
            Console.WriteLine("\nInvalid price format!");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter Start Date (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
        {
            Console.WriteLine("\nInvalid date format!");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter End Date (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
        {
            Console.WriteLine("\nInvalid date format!");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("\nInvalid Customer ID!");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("\nInvalid Product ID!");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter User ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("\nInvalid User ID!");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter Status Type ID: ");
        if (!int.TryParse(Console.ReadLine(), out int statusTypeId))
        {
            Console.WriteLine("\nInvalid Status Type ID!");
            Console.ReadKey();
            return;
        }

        if (string.IsNullOrWhiteSpace(projectName))
        {
            Console.WriteLine("\nProject Name is required!");
            Console.ReadKey();
            return;
        }

        var registrationForm = new ProjectRegistrationForm
        {
            ProjectName = projectName,
            TotalPrice = totalPrice,
            StartDate = startDate,
            EndDate = endDate,
            CustomerId = customerId,
            ProductId = productId,
            UserId = userId,
            StatusTypeId = statusTypeId
        };

        var result = await _projectService.CreateProjectAsync(registrationForm);
        Console.WriteLine(result ? "\nProject created successfully!" : "\nProject creation failed!");
        Console.ReadKey();
    }

    public async Task ViewProjects()
    {
        Console.Clear();
        Console.WriteLine("=== VIEW ALL PROJECTS ===");

        var projects = await _projectService.GetProjectsAsync();
        if (!projects.Any())
        {
            Console.WriteLine("No projects found.");
        }
        else
        {
            foreach (var project in projects)
            {
                // Förutsätter att Project-modellen har egenskaper: Id, ProjectName, ProjectNumber, TotalPrice, StartDate, EndDate
                Console.WriteLine($"ID: {project.Id}, Name: {project.ProjectName}, Number: {project.ProjectNumber}, " +
                                  $"Price: {project.TotalPrice:C}, Start: {project.StartDate:d}, End: {project.EndDate:d}");
            }
        }
        Console.ReadKey();
    }

    private async Task ViewSpecificProject()
    {
        Console.Clear();
        Console.WriteLine("=== VIEW PROJECT ===");

        Console.Write("Enter Project ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            Console.WriteLine("\nProject not found!");
        else
            Console.WriteLine($"ID: {project.Id}, Name: {project.ProjectName}, Project Number: {project.ProjectNumber}, " +
                              $"Price: {project.TotalPrice:Kr}, Start Date: {project.StartDate:d}, End Date: {project.EndDate:d}");
        Console.ReadKey();
    }

    public async Task UpdateProject()
    {
        Console.Clear();
        Console.WriteLine("=== UPDATE PROJECT ===");

        Console.Write("Enter Project ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var existingProject = await _projectService.GetProjectByIdAsync(id);
        if (existingProject == null)
        {
            Console.WriteLine("\nProject not found!");
            Console.ReadKey();
            return;
        }

        Console.Write($"Enter new project name (leave empty to keep '{existingProject.ProjectName}'): ");
        var projectName = Console.ReadLine()?.Trim();
        projectName = string.IsNullOrWhiteSpace(projectName) ? existingProject.ProjectName : projectName;

        Console.Write($"Enter new total price (leave empty to keep '{existingProject.TotalPrice:C}'): ");
        var priceInput = Console.ReadLine()?.Trim();
        decimal totalPrice = existingProject.TotalPrice;
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
        {
            totalPrice = newPrice;
        }

       

        var updatedProject = new Project
        {
            Id = id,
            ProjectName = projectName,
            TotalPrice = totalPrice,
           
            StartDate = existingProject.StartDate,
            EndDate = existingProject.EndDate,
            CustomerId = existingProject.CustomerId,
            ProductId = existingProject.ProductId,
            UserId = existingProject.UserId,
            StatusTypeId = existingProject.StatusTypeId,
            ProjectNumber = existingProject.ProjectNumber  
        };

        var success = await _projectService.UpdateProjectAsync(updatedProject);
        Console.WriteLine(success ? "\nProject updated successfully!" : "\nFailed to update project.");
        Console.ReadKey();
    }


    public async Task DeleteProject()
    {
        Console.Clear();
        Console.WriteLine("=== DELETE PROJECT ===");

        Console.Write("Enter Project ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var success = await _projectService.DeleteProjectAsync(id);
        Console.WriteLine(success ? "\nProject deleted successfully!" : "\nFailed to delete project.");
        Console.ReadKey();
    }
}
