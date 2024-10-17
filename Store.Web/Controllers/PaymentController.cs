using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Store.Service.BasketService;
using Store.Service.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger )
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        => Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_9c6fe7fb3e3b5c2f29d7ca12cee0386fcbcba6c1cb491bd87d5e6ded74349494";
             
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);
                PaymentIntent paymentIntent;
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed: ", paymentIntent.Id);
                    var order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("order updated to payment Succeeded : ", order.Id);

                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed: ", paymentIntent.Id);
                   var order = await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("order updated to payment failed : ", order.Id);


                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentCreated)
                {
                    _logger.LogInformation("Payment Created ");
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}

