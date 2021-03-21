using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Order
{
    public class OrderProduct : Entity
    {
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Vat { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithVat { get; set; }
    }
}
