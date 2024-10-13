using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.BasketService;
using Store.Service.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService
{
    public class OrderService : IorderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;

        public OrderService(IBasketService basketService, IUnitOfWork unitOfWork,IMapper autoMapper)
        {
            _basketService = basketService;
           _unitOfWork = unitOfWork;
           _autoMapper = autoMapper;
        }
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            var basket = await _basketService.GetBaskeAsync(input.BasketId);

            if (basket is null)
                throw new Exception("Basket Not Exist");

            #region fill order item list with items in basket
            var orderItems = new List<OrderItemDto>();
            foreach(var basketItem in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product,int>().GetByIdAsync(basketItem.ProductId); 
                if(productItem is null)
                    throw new Exception($"Product with Id:{basketItem.ProductId} Not Exist");

                var itemOrder = new ProductItem
                {

                    ProductId = productItem.Id,
                    ProductName = productItem.Name,
                    PictureUrl = productItem.PictureUrl

                };

                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    ItemOrdered = itemOrder
                };
                var mappedOrderItem = _autoMapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedOrderItem);
            }
            #endregion

            #region Get Delivery Method
            var delivaryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);
            if (delivaryMethod is null)
                throw new Exception("Delivery Method Not Provided");


            #endregion

            #region Calculate subtotal
            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);
            #endregion

            #region To DO ==> Payment

            #endregion

            #region Creat Order
            var mappedShippingAddress = _autoMapper.Map<ShippingAddress>(input.ShippingAddress);
            var mappedOrderItems = _autoMapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                DeliveryMethodId = delivaryMethod.Id,
                ShippingAddress = mappedShippingAddress,
                BuyerEmail = input.BuyerEmail,
                BasketId = input.BasketId,
                OrderItems = mappedOrderItems,
                SubTotal = subTotal
            };

            await _unitOfWork.Repository<Order,Guid>().AddAsync(order);
            await _unitOfWork.CompleteAsync(); 
            var mappedOrder = _autoMapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
            #endregion
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
            => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrderForUserAsyn(string buyerEmail)
        {
           var specs=new OrderWithItemSpecification(buyerEmail);   
           var orders = await _unitOfWork.Repository<Order,Guid>().GetAllWithSpecAsync(specs);
            if (!orders.Any())
                throw new Exception("You Don't have any orders yet");

            var mappedOrders = _autoMapper.Map<List<OrderDetailsDto>>(orders);
            return mappedOrders;
        }

        public async Task<OrderDetailsDto> GetOrderByIdasync(Guid id)
        {
            var specs = new OrderWithItemSpecification(id);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecByIdAsync(specs);
            if (order is null)
                throw new Exception($"There is no order for ID {id}");

            var mappedOrder = _autoMapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }
    }
}
