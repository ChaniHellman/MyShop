using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace OurShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdrsController : Controller
    {
         IOrderService _OrderService;
        IMapper _mapper;

        public OrdrsController(IOrderService OrderService, IMapper mapper)
        {
            _OrderService = OrderService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<returnOrderDto> getOrderById(int id)
        {
            Order order = await _OrderService.getOrderById(id);
            return _mapper.Map<Order, returnOrderDto>(order);
        }


        [HttpPost]
        public async Task<ActionResult<Order>> addOrder(OrderPostDto order)
        {
            try
            {
                Order returnedOrder = await _OrderService.addOrder(_mapper.Map<OrderPostDto, Order>(order));
                return CreatedAtAction(nameof(getOrderById), new { id = order.userId }, _mapper.Map<Order, returnOrderDto>(returnedOrder));

            }
            catch (InvalidOrderException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

       

    }
}
