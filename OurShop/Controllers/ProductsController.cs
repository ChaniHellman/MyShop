using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTO;

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
         IProductService _productService;
         IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            List<Product> checkProduct = await _productService.getProducts();
            if (checkProduct != null)
                return Ok(_mapper.Map<List<Product>, List<ProductDto>>(checkProduct));
            else
                return NotFound();
        }

   
    }
}
