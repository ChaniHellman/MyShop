using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using DTO;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductrepository _productRepository;
        private readonly IDistributedCache _cache;
        public ProductService(IProductrepository productRepository, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task<List<ProductDto>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            // Create a cache key based on filter parameters
            var key = $"products:{desc}:{minPrice}:{maxPrice}:{string.Join(",", categoryIds ?? new int?[0])}";
            var cached = await _cache.GetStringAsync(key);
            if (cached != null)
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<ProductDto>>(cached);
            }
            var products = await _productRepository.getProducts(desc, minPrice, maxPrice, categoryIds);
            var productDtos = products.Select(p => new ProductDto(
                p.ProductId,
                p.ProductName,
                p.Price,
                p.Description,
                p.ImageUrl,
                p.Category?.CategoryName ?? string.Empty
            )).ToList();
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) };
            await _cache.SetStringAsync(key, System.Text.Json.JsonSerializer.Serialize(productDtos), options);
            return productDtos;
        }
    }
}

