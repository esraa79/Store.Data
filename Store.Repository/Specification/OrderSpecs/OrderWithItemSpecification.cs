﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Store.Data.Entities.OrderEntities;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithItemSpecification:BaseSpecification<Order>
    {
        public OrderWithItemSpecification(string buyerEmail)
            :base(order=>order.BuyerEmail==buyerEmail) 
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.OrderItems);
            AddOrderByDesc(order => order.OrderDate);

        }

        public OrderWithItemSpecification(Guid id, string buyerEmail)
           : base(order => order.Id == id)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.OrderItems);
            

        }

    }
}
