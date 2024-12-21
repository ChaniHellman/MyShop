using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        _328177589ShopApiContext _shopContext;
        public CategoryRepository(_328177589ShopApiContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<Category>> getCategories()
        {
            List<Category> allCategories = await _shopContext.Categories.Include(c=>c.Products).ToListAsync<Category>();
            return allCategories;
        }
    }
}
