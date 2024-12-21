using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DTO;

public record OrderPostDto(int orderId,DateOnly orderDate, int userId,int orderSum,List<OrderPostDto> OrderItems);
public record returnOrderDto(DateOnly orderDate, int userId, List<OrderItemDto> OrderItems);
public record returnOrdersListDto( DateOnly orderDate, int orderId);


