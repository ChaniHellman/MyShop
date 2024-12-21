using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _OrderRepository;
        public OrderService(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public async Task<Order> getOrderById(int id)
        {
            Order order = await _OrderRepository.getOrderById(id);
            return order;


        }
        public async Task<Order> addOrder(Order order)
        {
            return await _OrderRepository.addOrder(order);

        }

    }
}
