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
            CreateMap<DTO.Category, Category>().ReverseMap();
            CreateMap<DTO.Location, Location>().ReverseMap();
            CreateMap<DTO.Menu, Menu>().ReverseMap();
            CreateMap<DTO.MenuDetail, MenuDetail>().ReverseMap();
            CreateMap<DTO.Order, Order>().ReverseMap();
            CreateMap<DTO.OrderDetail, OrderDetail>().ReverseMap();
            CreateMap<DTO.Product, Product>().ReverseMap();
            CreateMap<DTO.Role, Role>().ReverseMap();
            CreateMap<DTO.Shift, Shift>().ReverseMap();
            CreateMap<DTO.Shipper, Shipper>().ReverseMap();
            CreateMap<DTO.Slot, Slot>().ReverseMap();
            CreateMap<DTO.Store, Store>().ReverseMap();
            CreateMap<DTO.User, User>().ReverseMap();
        }
    }
}
