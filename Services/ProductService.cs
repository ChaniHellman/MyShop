using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductrepository _productRepository;
        public ProductService(IProductrepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<List<Product>> getProducts()
        {
            return await _productRepository.getProducts();

        }


    }
}

