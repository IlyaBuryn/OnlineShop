using BusinessLogic.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class Calculation
    {
        public static Dictionary<Dto_Product, int> ViewToDictionary(List<Dto_Product> products)
        {
            var productsGroup = new Dictionary<Dto_Product, int>();
            foreach (var item in products)
            {
                if (productsGroup.ContainsKey(item))
                {
                    productsGroup[item] = productsGroup[item] + 1;
                }
                else
                {
                    productsGroup.Add(item, 1);
                }
            }
            return productsGroup;
        }

        public static Dictionary<Dto_Product, int> ViewToDictionary(List<Dto_OrderDetails> products)
        {
            var productsGroup = new Dictionary<Dto_Product, int>();
            foreach (var item in products)
            {
                if (productsGroup.ContainsKey(item.Product))
                {
                    productsGroup[item.Product] = productsGroup[item.Product] + 1;
                }
                else
                {
                    productsGroup.Add(item.Product, 1);
                }
            }
            return productsGroup;
        }

        public static decimal GetSum(Dictionary<Dto_Product, int> products)
        {
            decimal sum = 0;
            foreach (var item in products)
                for (int i = 0; i < item.Value; i++)
                    sum += item.Key.Price;
            return sum;
        }
    }
}
