using Entities;

namespace Repositories
{
    public interface IProductrepository
    { 
        Task<List<Product>> getProducts();
    
    }
}