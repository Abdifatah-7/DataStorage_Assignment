using Business.Interfaces;
using Business.Models;

namespace PresentationConsoleApp.Dialogs;

public class ProductMenu(IProductService productService)
{
    private readonly IProductService _productService = productService;

    public async Task Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Products ===");
            Console.WriteLine("1. Create Product");
            Console.WriteLine("2. View all Products");
            Console.WriteLine("3. View Specific Product");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateProduct();
                    break;
                case "2":
                    await ViewProducts();
                    break;
                case "3":
                    await ViewSpecificProduct();
                    break;
                case "4":
                    await UpdateProduct();
                    break;
                case "5":
                    await DeleteProduct();
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


    public async Task CreateProduct()
    {
        Console.Clear();
        Console.WriteLine("=== CREATE A PRODUCT ===");

        Console.Write("Enter Product Name: ");
        var productName = Console.ReadLine()?.Trim();

        Console.Write("Enter Product Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice))
        {
            Console.WriteLine("\nInvalid price format!");
            Console.ReadKey();
            return;
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            Console.WriteLine("\nAll fields are required!");
            Console.ReadKey();
            return;
        }

        var registrationForm = new ProductRegistrationForm
        {
            ProductName = productName,
            ProductPrice = productPrice
        };

        var result = await _productService.CreateProductAsync(registrationForm);

        Console.WriteLine(result ? "\nProduct was created successfully!" : "\nProduct was not created.");
        Console.ReadKey();
    }

    public async Task ViewProducts()
    {
        var products = await _productService.GetProductsAsync();

        Console.Clear();
        Console.WriteLine("=== View Products ===");

        if (!products.Any())
        {
            Console.WriteLine("No products found.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product!.Id}, Name: {product.ProductName}, Price: {product.ProductPrice:C}");
            }
        }
        Console.ReadKey();
    }

    public async Task ViewSpecificProduct()
    {
        Console.Clear();
        Console.WriteLine("=== View Product ===");

        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            Console.WriteLine("\nProduct not found!");
        else
            Console.WriteLine($"ID: {product!.Id}, Name: {product.ProductName}, Price: {product.ProductPrice:C}");

        Console.ReadKey();
    }

    public async Task UpdateProduct()
    {
        Console.Clear();
        Console.WriteLine("=== UPDATE PRODUCT ===");

        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var existingProduct = await _productService.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            Console.WriteLine("\nProduct not found!");
            Console.ReadKey();
            return;
        }

        Console.Write($"Enter new product name (leave empty to keep '{existingProduct.ProductName}'): ");
        var productName = Console.ReadLine()?.Trim();
        productName = string.IsNullOrWhiteSpace(productName) ? existingProduct.ProductName : productName;

        Console.Write($"Enter new price (leave empty to keep '{existingProduct.ProductPrice:Kr}'): ");
        var priceInput = Console.ReadLine()?.Trim();
        decimal productPrice = existingProduct.ProductPrice;

        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out var newPrice))
        {
            productPrice = newPrice;
        }

        var updatedProduct = new Product
        {
            Id = id,
            ProductName = productName,
            ProductPrice = productPrice
        };

        var success = await _productService.UpdateProductAsync(updatedProduct);
        Console.WriteLine(success ? "\nProduct updated successfully!" : "\nFailed to update product.");
        Console.ReadKey();
    }


    public async Task DeleteProduct()
    {
        Console.Clear();
        Console.WriteLine("=== DELETE PRODUCT ===");

        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nInvalid ID format!");
            Console.ReadKey();
            return;
        }

        var success = await _productService.DeleteProductAsync(id);
        Console.WriteLine(success ? "\nProduct deleted successfully!" : "\nFailed to delete product.");
        Console.ReadKey();
    }
}
