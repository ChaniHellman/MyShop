using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        ICategoryService _CategoryService;
        IMapper _mapper;

        public CategoriesController(ICategoryService CategoryService, IMapper mapper )
        {
            _CategoryService = CategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            IEnumerable<Category> checkCategory = await _CategoryService.getCategories();
            if (checkCategory != null)
                return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<getCategoryDto>>(checkCategory));
            else
                return NotFound();
        }
    }
}
