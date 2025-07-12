using Entities;
using DTO;

namespace Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}