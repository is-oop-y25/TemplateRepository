using Shops.Modules;

namespace Shops.Services
{
    public interface IShopsService
    {
        Shop AddShop(string name, string address);
        Product AddProduct(string name);
        void ProductTransfer(Shop shop, string name, int price, int count);
        Shop FindShopWithMinPrice(string productName);
        void BuyProduct(string productName, int count, User user);
        void ChangeProductPrice(Shop shop, string name, int newPrice);
    }
}