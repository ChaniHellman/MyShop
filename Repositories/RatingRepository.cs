using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        MyShopContext _shopContext;

        public RatingRepository(MyShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public async Task<Rating> addRating(Rating rating)
        {
            await _shopContext.Ratings.AddAsync(rating);
            await _shopContext.SaveChangesAsync();
            return (rating);
        }
    }
}
