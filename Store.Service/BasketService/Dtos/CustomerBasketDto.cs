﻿using Store.Repository.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.BasketService
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public int? DeliveryMethods { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
    }
}
