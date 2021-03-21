using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Rules
{
    internal class ProductVatMustBeConsistentRule : IBusinessRule
    {
        private readonly decimal _price;
        private readonly decimal _priceWithVat;

        public ProductVatMustBeConsistentRule(decimal price, decimal priceWithVat)
        {
            _price = price;
            _priceWithVat = priceWithVat;
        }

        public string Message => "Price and vat not equal to PriceWithVat value!";

        public bool IsBroken()
        {
            //if (_price * Consts.Price.VatRate == _priceWithVat)
            return false;
            //else
            //  return true;
        }
    }
}
