﻿using AutoMapper;
using Store.Data.Entities.OrderEntities;
using Store.Service.Services.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService.Dtos
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderDetailsDto>()
                     .ForMember(dest => dest.DeliveryMethodName, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                    .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(dest => dest.ItemOrderedId, options => options.MapFrom(src => src.ItemOrdered.ProductId))
                   .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ItemOrdered.ProductName))
                    .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.ItemOrdered.PictureUrl))
                   .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureULResolver>()).ReverseMap();

        }

    }
}
