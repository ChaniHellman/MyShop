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
        _328177589ShopApiContext _shopContext;

        public RatingRepository(_328177589ShopApiContext shopContext)
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
