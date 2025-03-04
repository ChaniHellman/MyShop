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
        MyShopContext _shopContext;
        public CategoryRepository(MyShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<Category>> getCategories()
        {
            List<Category> allCategories = await _shopContext.Categories.ToListAsync();
            return allCategories;
        }
    }
}
