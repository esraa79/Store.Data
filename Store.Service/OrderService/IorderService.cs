using Store.Data.Entities;
using Store.Service.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService
{
    public interface IorderService
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderDto input);
        Task<IReadOnlyList<OrderDetailsDto>> GetAllOrderForUserAsyn(string BuyerEmail);
        Task<OrderDetailsDto> GetOrderByIdasync(Guid id,string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();

    }
}
