using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services;

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        ICategoryService _CategoryService;
        IMapper _mapper;
        private readonly IMemoryCache _cache;


        public CategoriesController(ICategoryService CategoryService, IMapper mapper, IMemoryCache cache)
        {
            _CategoryService = CategoryService;
            _mapper = mapper;
            _cache = cache;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<getCategoryDto>>> Get()
        {
            if (!_cache.TryGetValue("CategoriesCache", out IEnumerable<Category> categories))
            {
                categories = await _CategoryService.getCategories();

                if (categories == null || !categories.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                _cache.Set("CategoriesCache", categories, cacheEntryOptions);
            }

            return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<getCategoryDto>>(categories));
        }

       
    }
}
