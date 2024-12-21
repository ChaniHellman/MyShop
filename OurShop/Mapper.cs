using AutoMapper;
using DTO;
using Entities;
namespace OurShop
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<User, UserByIdDto>();
            CreateMap<addUserDto,User>();
            CreateMap<UserByIdDto, User>();
            CreateMap<addUserDto,returnPostUserDto>();
            CreateMap<User, returnLoginUserDto>();
            CreateMap<Category, getCategoryDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto,OrderItem > ();

            CreateMap<Order, OrderPostDto>();
            CreateMap< OrderPostDto, Order>();

            CreateMap<Order,returnOrderDto>();
            CreateMap<OrderPostDto, returnOrderDto>();


        }
    }
}
