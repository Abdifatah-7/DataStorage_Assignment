using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegistrationForm form);
        Task<bool> DeleteCustomerAsync(int id);
        Task<Customer?> GetCustomerByCustomerNameAsync(string customerEmail);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer?>> GetCustomersAsync();
        Task<bool> UpdateCustomerAsync(Customer customer);
    }
}