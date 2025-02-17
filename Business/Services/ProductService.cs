using Business.Factories;
using Business.Models;
using Data.Repositories;

namespace Business.Services;

public class ProductService(ProductRepository productRepository)
{
    private readonly ProductRepository _productRepository = productRepository;

    // Skapa en produkt
    public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
    {
        var existingProduct = await _productRepository.GetAsync(p => p.ProductName == form.ProductName);
        if (existingProduct != null)
            return false;

        var productEntity = ProductFactory.Create(form);
        await _productRepository.CreateAsync(productEntity!);
        return true;
    }

    // Hämta alla produkter
    public async Task<IEnumerable<Product?>> GetProductsAsync()
    {
        var productEntities = await _productRepository.GetAllAsync();
        return productEntities.Select(ProductFactory.Create)!;
    }

    // Hämta produkt via ID
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var productEntity = await _productRepository.GetAsync(p => p.Id == id);
        return ProductFactory.Create(productEntity!);
    }

    // Uppdatera en produkt
    public async Task<bool> UpdateProductAsync(Product product)
    {
        try
        {
            var existingProduct = await _productRepository.GetAsync(p => p.Id == product.Id);
            if (existingProduct == null)
                return false;

            ProductFactory.UpdateEntity(existingProduct, product);

            var result = await _productRepository.UpdateAsync(p => p.Id == product.Id, existingProduct);
            return result != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Ta bort en produkt
    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            var productEntity = await _productRepository.GetAsync(p => p.Id == id);
            if (productEntity == null)
                return false;

            return await _productRepository.DeleteAsync(p => p.Id == id);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
