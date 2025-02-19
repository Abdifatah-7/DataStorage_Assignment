
using Business.Interfaces;

namespace PresentationConsoleApp.Dialogs;

public  class MenuService(ICustomerService customerService, IProductService productService, IProjectService projectService, IUserService userService, IStatusTypeService statusTypeService)
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProductService _productService = productService;
    private readonly IProjectService _projectService = projectService;
    private readonly IUserService _userService = userService;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    public async Task  ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Mattin-Lassei Group AB ===");
            Console.WriteLine("1. Manage Project");
            Console.WriteLine("2. Manage Customer");
            Console.WriteLine("3. Manage User");
            Console.WriteLine("4. Manage Product");
            Console.WriteLine("5. Manage Status");
            Console.WriteLine("6. Quit application");
            Console.Write("SELSCT YOUR OPTION: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var projectMenu = new ProjectMenu(_projectService);
                    await projectMenu.Show();
                    break;
                case "2":
                    var customerMenu = new CustomerMenu(_customerService);
                    await customerMenu.Show();
                    break;
                case "3":
                    var userMenu = new UserMenu(_userService);
                    await userMenu.Show();
                    break;
                case "4":
                    var productMenu = new ProductMenu(_productService);
                    await productMenu.Show();
                    break;
                case "5":
                    var statusTypeMenu = new StatusTypeMenu(_statusTypeService);
                    await statusTypeMenu.Show();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("INVALID OPTION. PLEASE TRY AGAIN..");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
