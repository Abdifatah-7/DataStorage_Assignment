

using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresentationConsoleApp.Dialogs;




var serviceCollection = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Bibliotek\\DataStorage_Assignment\\Data\\Databases\\Local_Database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"))

// Registrera repositories
.AddScoped<ICustomerRepository, CustomerRepository>()
.AddScoped<IProductRepository, ProductRepository>()
.AddScoped<IProjectRepository, ProjectRepository>()
.AddScoped<IUserRepository, UserRepository>()
.AddScoped<IStatusTypeRepository, StatusTypeRepository>()

// Registrera services
.AddScoped<ICustomerService, CustomerService>()
.AddScoped<IProductService, ProductService>()
.AddScoped<IProjectService, ProjectService>()
.AddScoped<IUserService, UserService>()
.AddScoped<IStatusTypeService, StatusTypeService>()

// Registrera MenuService
.AddScoped<MenuService>();

// Bygg upp ServiceProvider
using var serviceProvider = serviceCollection.BuildServiceProvider();

// Hämta MenuService och starta huvudmenyn
var menuService = serviceProvider.GetRequiredService<MenuService>();
await menuService.ShowMainMenu();

