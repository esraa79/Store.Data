using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Store.Data.Entities;
using Store.Service.HandleResponses;
using Store.Service.OrderService;
using Store.Service.OrderService.Dtos;
using System.Security.Claims;

namespace Store.Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IorderService _orderService;

        public OrdersController(IorderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderDetailsDto>> CreateOrderAsync(OrderDto input)
        {
            var order = await _orderService.CreateOrderAsync(input);
            if (order is null)
                return BadRequest(new Response(400, "Error While Creating Oredr"));
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAllOrderforUserAsync(OrderDto input)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetAllOrderForUserAsyn(email);
           return Ok(orders);

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodsAsync()
            => Ok(await _orderService.GetAllDeliveryMethodsAsync());

        [HttpGet]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdAsync(Guid Id, string buerEmail)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrderByIdasync(Id, email);
            return Ok(orders);

        }
    }
}
