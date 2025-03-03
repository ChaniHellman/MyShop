using Entities;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException(string message) : base(message) { }
    }
    public class OrderService : IOrderService
    {
        IProductrepository _ProductRepository;
        private readonly ILogger<OrderService> _logger;
        IOrderRepository _OrderRepository;
        public OrderService(IOrderRepository OrderRepository, IProductrepository ProductRepository, ILogger<OrderService> logger)
        {
            _ProductRepository = ProductRepository;
            _logger = logger;
            _OrderRepository = OrderRepository;
        }

        public async Task<Order> getOrderById(int id)
        {
            Order order = await _OrderRepository.getOrderById(id);
            return order;


        }
        public async Task<Order> addOrder(Order order)
        {
            if (!await checkTotalPrice(order))
            {
                throw new InvalidOrderException("Total price does not match the expected order sum.");
            }

            return await _OrderRepository.addOrder(order);

        }

        private async Task<bool> checkTotalPrice(Order order)
        {
            Decimal totalPrice = 0;
            foreach (OrderItem item in order.OrderItems)
            {
                Product product = await _ProductRepository.getById(item.ProductId);
                if (product != null)
                {
                    totalPrice += product.Price * item.Quantity;
                }

            }
            if (totalPrice != order.OrderSum)
            {
                _logger.LogWarning("Total price of order did not match the orderSum. Client: " + order.UserId); //logger doesnt send email!!! write critical.....
            
                return false;
            }
            return true;
        }

    }

}

