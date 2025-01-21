using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;




namespace Repositories
{
    public class ProductRepository : IProductrepository
    {
        _328177589ShopApiContext _shopContext;
        public ProductRepository(_328177589ShopApiContext shopContext)
        {
            _shopContext = shopContext;
        }


        public async Task<List<Product>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var query = _shopContext.Products.Include(p => p.Category).Where(product =>
             (desc == null ? (true) : (product.ProductName.Contains(desc)))
             && ((minPrice == null) ? (true) : (product.Price >= minPrice))
             && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
             && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
            .OrderBy(product => product.Price);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            return products;
        }


    }
}
