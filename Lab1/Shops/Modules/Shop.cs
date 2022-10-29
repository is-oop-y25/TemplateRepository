using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Services;

namespace Shops.Modules
{
    public class Shop
    {
        private Dictionary<int, ProductProperty> _productsList;
        public Shop(string shopName, int id, string address, int money)
        {
            Name = shopName;
            Id = id;
            Address = address;
            Money = money;
            _productsList = new Dictionary<int, ProductProperty>();
        }

        public int Money { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }

        public void AddProductToShop(Product product, ProductProperty productProperty)
        {
            if (!_productsList.TryAdd(product.Id, productProperty))
            {
                _productsList[product.Id].Count++;
            }
        }

        public ProductProperty PriceAndCount(Product product, int price, int count)
        {
            var productProperty = new ProductProperty(product, price, count);
            return productProperty;
        }

        public Dictionary<int, ProductProperty> GetProductList()
        {
            return _productsList;
        }

        public void RemoveProductFromShop(int id)
        {
            if (_productsList[id].Count > 1)
            {
                _productsList[id].Count--;
            }
            else
            {
                _productsList.Remove(id);
            }
        }

        public int ChangePrice(string name, int newPrice)
        {
            foreach (KeyValuePair<int, ProductProperty> curProduct in _productsList)
            {
                    if (curProduct.Value.RefProduct == name)
                    {
                        curProduct.Value.Price = newPrice;
                    }

                    return curProduct.Key;
            }

            throw new Exception("Product doesn't exist");
        }

        public int GetPrice(Shop shop, string name)
        {
            foreach (var curProduct in shop._productsList.Where(curProduct => curProduct.Value.RefProduct == name))
            {
                return curProduct.Value.Price;
            }

            throw new Exception("Product doesn't exist");
        }

        public bool Contains(string name)
        {
            foreach (var curProduct in _productsList.Where(curProduct => curProduct.Value.RefProduct == name))
            {
                return true;
            }

            return false;
        }
    }
}