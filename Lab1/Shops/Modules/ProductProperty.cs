namespace Shops.Modules
{
    public class ProductProperty
    {
        public ProductProperty(Product refProduct, int price, int count)
        {
            RefProduct = refProduct.ProductName;
            Price = price;
            Count = count;
        }

        public string RefProduct { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }
    }
}