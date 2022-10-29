using Shops.Modules;
using Shops.Services;
using Xunit;

namespace Shops.Tests
{
    public class ShopsTests
    {
        private ShopsService _shopsService = new ShopsService();

        [Fact]
        public void AddShop_AddShopAndTransferProduct()
        {
            Shop testShop1 = _shopsService.AddShop("Semishagoff", "Engelsa 96");
            Shop testShop2 = _shopsService.AddShop("Magnit", "Engelsa 106");
            Product testProduct = _shopsService.AddProduct("Milk");
            _shopsService.ProductTransfer(testShop1, "Milk", 20, 100);

            Assert.Contains(testShop2.Id, new[] { 2 });
            Assert.Contains(testShop1.Contains("Milk"), new[] { true });
        }

        [Fact]
        public void ChangePrice()
        {
            int newPrice = 40;
            Shop testShop1 = _shopsService.AddShop("Semishagoff", "Engelsa 96");
            Product testProduct = _shopsService.AddProduct("Milk");
            _shopsService.ProductTransfer(testShop1, "Milk", 20, 10);
            testShop1.ChangePrice(testProduct.ProductName, newPrice);

            Assert.Contains(testShop1.GetPrice(testShop1, testProduct.ProductName), new[] { newPrice });
        }

        [Fact]
        public void FindShopWithMinCost()
        {
            Shop testShop1 = _shopsService.AddShop("Semishagoff", "Engelsa 96");
            Shop testShop2 = _shopsService.AddShop("Magnit", "Engelsa 106");
            Product testProduct1 = _shopsService.AddProduct("Milk");
            _shopsService.ProductTransfer(testShop1, "Milk", 20, 10);
            _shopsService.ProductTransfer(testShop2, "Milk", 40, 10);

            Shop shopWithMinValue = _shopsService.FindShopWithMinPrice(testProduct1.ProductName);

            Assert.Contains(shopWithMinValue.Name, new[] { "Semishagoff" });
        }

        [Fact]
        public void BuySomeProducts_CreateShopAndCreateProductsAndBuy()
        {
            Shop testShop1 = _shopsService.AddShop("Semishagoff", "Engelsa 96");
            var testUser = new User(200);
            Product testProduct1 = _shopsService.AddProduct("Milk");
            _shopsService.ProductTransfer(testShop1, "Milk", 10, 100);
            _shopsService.BuyProduct("Milk", 10, testUser);
            Assert.Throws<Exception>(() =>
            {
                _shopsService.BuyProduct("Milk", 100, testUser);
            });
            Assert.Equal(100, testUser.Money);
        }
    }
}