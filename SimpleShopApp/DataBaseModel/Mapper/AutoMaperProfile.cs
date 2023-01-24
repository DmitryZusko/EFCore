namespace DataBaseModel.Mapper
{
    using AutoMapper;
    using DataBaseModel.DTOModels;
    using DataBaseModel.Models;
    public class AutoMaperProfile : Profile
    {
        public AutoMaperProfile()
        {
            CreateMap<Seller, SellerDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderDetailDto>().ReverseMap();
        }
    }
}
