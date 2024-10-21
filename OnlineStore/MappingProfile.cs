using AutoMapper;
using OnlineStore.Dtos;
using OnlineStore.Entities;

namespace OnlineStore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductToDto, Products>().ReverseMap();

            CreateMap<Orders, GetAllOrderDto>();
            CreateMap<OrdersProducts, OrderProductToDto>();
            CreateMap<Products, ProductOrderDto>();
        }
    }
}
