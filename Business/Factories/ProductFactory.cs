using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProductFactory
{
    public static ProductEntity? Create(ProductRegistrationForm form) => form == null ? null : new()
    {
        ProductName = form.ProductName,
        ProductPrice = form.ProductPrice
    };

    public static Product? Create(ProductEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        ProductName = entity.ProductName,
        ProductPrice = entity.ProductPrice
    };

    public static void UpdateEntity(ProductEntity existingEntity, Product updatedProduct)
    {
        existingEntity.ProductName = updatedProduct.ProductName;
        existingEntity.ProductPrice = updatedProduct.ProductPrice;
    }
}
