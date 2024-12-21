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

        //[HttpPost]
        //public async Task<ActionResult<Order>> addOrder(Order order)
        //{

        //    await _OrderService.addOrder(order);
        //    return CreatedAtAction(nameof(getOrderById), new { id = order.OrderId },_mapper.Map<Order, returnOrderDto>(order));
        //}

        [HttpPost]
        public async Task<ActionResult<Order>> addOrder(OrderPostDto order)
        {

            await _OrderService.addOrder(_mapper.Map<OrderPostDto, Order>(order));
            return CreatedAtAction(nameof(getOrderById), new { id = order.userId }, _mapper.Map<OrderPostDto, returnOrderDto>(order));
        }


    }
}
