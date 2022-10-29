namespace Shops.Modules
{
    public class Product
    {
        public Product(string productName, int id)
        {
            ProductName = productName;
            Id = id;
        }

        public int Id { get; set; }

        public string ProductName { get; set; }
    }
}