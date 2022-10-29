using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Modules;

namespace Shops.Services
{
    public class ShopsService : IShopsService
    {
        private readonly List<Shop> _shops = new List<Shop>();
        private readonly List<Product> _products = new List<Product>();
        private int _shopId = 1;
        private int _productId = 0;

        public Shop AddShop(string name, string address)
        {
            int money = 0;
            var shop = new Shop(name, _shopId++, address, money);
            _shops.Add(shop);
            return shop;
        }

        public Product AddProduct(string name)
        {
            var product = new Product(name, _productId++);
            _products.Add(product);
            return product;
        }

        public void ProductTransfer(Shop shop, string name, int price, int count)
        {
            ProductProperty productProperty = shop.PriceAndCount(AddProduct(name), price, count);
            shop.AddProductToShop(AddProduct(name), productProperty);
        }

        public Shop FindShopWithMinPrice(string productName)
        {
            int minValue = (from curShop in _shops
                from curProduct in curShop.GetProductList()
                where curProduct.Value.RefProduct == productName
                select curProduct.Value.Price)
                .Prepend(int.MaxValue)
                .Min();

            foreach (var curShop in from curShop in _shops from curProduct in curShop.GetProductList() where curProduct.Value.Price == minValue select curShop)
            {
                return curShop;
            }

            throw new Exception("Shop doesn't exist");
        }

        public void BuyProduct(string productName, int count, User user)
        {
            foreach (Shop curShop in _shops)
            {
                foreach (var curProduct in curShop.GetProductList().Where(curProduct => curProduct.Value.RefProduct == productName))
                {
                    if (curProduct.Value.Count > count)
                    {
                        int canBuy = curProduct.Value.Price * count;
                        if (user.Money > canBuy)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                curShop.RemoveProductFromShop(curProduct.Key);
                                user.Money -= curProduct.Value.Price;
                                curShop.Money += curProduct.Value.Price;
                            }
                        }
                        else
                        {
                            throw new Exception("You can't buy all properties!");
                        }
                    }
                    else
                    {
                        throw new Exception("Not enough properties!");
                    }
                }
            }
        }

        public void ChangeProductPrice(Shop shop, string name, int newPrice)
        {
            shop.ChangePrice(name, newPrice);
        }
    }
}