namespace Marketplace.Common
{
    public static class Consts
    {
        public static class Order
        {
            public static string Placed = "ORDER_PLACED";
            public static string Created = "ORDER_CREATED";
            public static string Collecting = "ORDER_COLLECTING";
            public static string InTransit = "ORDER_INTRANSIT";
            public static string Delivered = "ORDER_DELIVERED";
        }

        public static class Price
        {
            public static decimal VatRate = 0.18M;
        }
    }
}
