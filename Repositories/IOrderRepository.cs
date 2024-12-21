using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> getOrderById(int id);
        Task<Order> addOrder(Order order);
    }
}