using Entities;

namespace Repositories
{
    public interface IProductrepository
    {
        Task<List<Product>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);


    }
}