using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace Service
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, DTO.Category>().ReverseMap();
            CreateMap<Location, DTO.Location>().ReverseMap();
            CreateMap<Menu, DTO.Menu>().ReverseMap();
            CreateMap<MenuDetail, DTO.MenuDetail>().ReverseMap();
            CreateMap<Order, DTO.Order>().ReverseMap();
            CreateMap<OrderDetail, DTO.OrderDetail>().ReverseMap();
            CreateMap<Product, DTO.Product>().ReverseMap();
            CreateMap<Role, DTO.Role>().ReverseMap();
            CreateMap<Shift, DTO.Shift>().ReverseMap();
            CreateMap<Shipper, DTO.Shipper>().ReverseMap();
            CreateMap<Slot, DTO.Slot>().ReverseMap();
            CreateMap<Store, DTO.Store>().ReverseMap();
            CreateMap<User, DTO.User>().ReverseMap();
        }
    }
}
