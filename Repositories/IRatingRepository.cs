using Entities;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task<Rating> addRating(Rating rating);
    }
}