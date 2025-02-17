using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
    {
        CustomerName = form.CustomerName,
        CustomerEmail = form.CustomerEmail,
    };

    public static Customer? Create(CustomerEntity Entities) => Entities == null ? null : new()
    {
        Id = Entities.Id,
        CustomerName = Entities.CustomerName,
        CustomerEmail = Entities.CustomerEmail,
    };

    public static void UpdateEntity(CustomerEntity existingEntity, Customer updatedCustomer)
    {
        existingEntity.CustomerName = updatedCustomer.CustomerName;
        existingEntity.CustomerEmail = updatedCustomer.CustomerEmail;
    }


}
