namespace Store.Repository.Basket.Models
{
    public class CustomerBasket
    {
        public string? Id { get; set; }
        public int? DeliveryMethods { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
