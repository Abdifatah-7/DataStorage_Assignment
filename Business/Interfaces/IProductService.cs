using Business.Models;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductRegistrationForm form);
        Task<bool> DeleteProductAsync(int id);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product?>> GetProductsAsync();
        Task<bool> UpdateProductAsync(Product product);
    }
}