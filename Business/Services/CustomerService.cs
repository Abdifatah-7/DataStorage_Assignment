
using Business.Factories;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{

    private readonly CustomerRepository _customerRepository = customerRepository;
     

    //Skapa en kund

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var existingCustomer = await _customerRepository.GetAsync(c => c.CustomerEmail == form.CustomerEmail);
        if (existingCustomer != null)
            return false;


        var customerEntity = CustomerFactory.Create(form);
        await _customerRepository.CreateAsync(customerEntity!);
        return true;
    }

    //Hämta upp kund efter Kund 

    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAllAsync();
        return customerEntities.Select(CustomerFactory.Create)!;
    }

    //Hämta upp kund efter Kund Id

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity!);
    }
    //Hämta upp kund efter kunds namn
    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerEmail)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerEmail == customerEmail);
        return CustomerFactory.Create(customerEntity!);
    }


    //Update 

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            var existingCustomer = await _customerRepository.GetAsync(c => c.Id == customer.Id);
            if (existingCustomer == null)
                return false;

            
            CustomerFactory.UpdateEntity(existingCustomer, customer);

            var result = await _customerRepository.UpdateAsync(x => x.Id == customer.Id, existingCustomer);
            return result != null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating customer: {ex.Message}");
            return false;
        }
    }



    //Delete
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        try
        {
            var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
            if (customerEntity == null)
                return false;

            return await _customerRepository.DeleteAsync(x => x.Id == id); 
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }

}
