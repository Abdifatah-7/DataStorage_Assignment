using Business.Interfaces;
using Business.Models;

namespace PresentationConsoleApp.Dialogs;

public class CustomerMenu(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Customers ===");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View all Customers");
            Console.WriteLine("3. View Specific Customer");
            Console.WriteLine("4. Update Customer");
            Console.WriteLine("5. Delete Customer");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateCustomer();
                    break;
                case "2":
                    await ViewCustomers();
                    break;
                case "3":
                    await ViewSpecificCustomers();
                    break;
                case "4":
                    await UpdateCustomer();
                    break;
                case "5":
                    await DeleteCustomer();
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


    public async Task CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== CREATE  Customers ===");

        Console.Write("Enter your Name : ");
        var customerName = Console.ReadLine()?.Trim();

        Console.Write("Enter your Email : ");
        var customerEmail = Console.ReadLine()? .Trim();

        if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(customerEmail))
        {
            Console.WriteLine("\n All fields are required!");
            Console.ReadKey();
            return;
        }

        var registrationForm = new CustomerRegistrationForm
        {
            CustomerName = customerName,
            CustomerEmail = customerEmail
        };

        var result = await _customerService.CreateCustomerAsync(registrationForm);

        if (result)
            Console.WriteLine("\n Customer was created succesfully");
        else
            Console.WriteLine("\n Customer was not created ");
        Console.ReadKey();
    }

    public async Task ViewCustomers()
    {
        var customers = await _customerService.GetCustomersAsync();

        Console.Clear();
        Console.WriteLine("=== View Customers ===");

        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
        }
        else
        {
            foreach (var customer in customers)
            {

                Console.Write($"ID:{customer!.Id} ");
                Console.Write($"Name: {customer.CustomerName} ");
                Console.Write($"Email: {customer.CustomerEmail} ");

            }
        }
        Console.ReadKey();
    }

    public async Task ViewSpecificCustomers()
    {

        Console.Clear();
        Console.WriteLine("=== View Customer ===");

        Console.Write("Enter Customer ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\n Invalid ID format!");
            Console.ReadKey();
            return;
        }


        var customer = await _customerService.GetCustomerByIdAsync( id );
        if ( customer == null )
            Console.WriteLine("\n Customer not found!");
        else

        Console.Write($"ID:{customer!.Id} ");
        Console.Write($"Name: {customer!.CustomerName} ");
        Console.Write($"Email: {customer.CustomerEmail} ");

        Console.ReadKey();
    }


    public async Task UpdateCustomer()
    {

        Console.Clear();
        Console.WriteLine("=== UPDATE CUSTOMER ===");

        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\n Invalid ID format!");
            Console.ReadKey();
            return;
        }

        var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
        if (existingCustomer == null)
        {
            Console.WriteLine("\n Customer not found!");
            Console.ReadKey();
            return;
        }

        Console.Write($"Enter new customer name (leave empty to keep '{existingCustomer.CustomerName}'): ");
        var customerName = Console.ReadLine()?.Trim();
        customerName = string.IsNullOrWhiteSpace(customerName) ? existingCustomer.CustomerName : customerName;//generas av Chat GTP

        Console.Write($"Enter new email (leave empty to keep '{existingCustomer.CustomerEmail}'): ");
        var customerEmail = Console.ReadLine()?.Trim();
        customerEmail = string.IsNullOrWhiteSpace(customerEmail) ? existingCustomer.CustomerEmail : customerEmail; //generas av Chat GTP

        //Användaren kan uppdatera endast ett fält och behålla det andra oförändrat.
        // Om användaren lämnar fältet tomt, används det gamla värdet istället.

        var updatedCustomer = new Customer
        {
            Id = id,
            CustomerName = customerName,
            CustomerEmail = customerEmail
        };


        var success = await _customerService.UpdateCustomerAsync(updatedCustomer);
        Console.WriteLine(success ? "\n Customer updated successfully!" : "\n Failed to update customer.");
        Console.ReadKey();
    }

    public async Task DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== DELETE CUSTOMER ===");

        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\n Invalid ID format!");
            Console.ReadKey();
            return;
        }

        var success = await _customerService.DeleteCustomerAsync(id);
        Console.WriteLine(success ? "\n Customer deleted successfully!" : "\n Failed to delete customer.");
        Console.ReadKey();
    }
}




