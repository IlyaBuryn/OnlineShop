using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogic.Maps
{
    public class OrderDetailsMapper
    {
        public static Dto_OrderDetails CastOrderDetailsToDto(OrderDetails item)
        {
            return new Dto_OrderDetails()
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId
                //Order = orders.GetOrderById(item.OrderId),
                //Product = prods.GetIncludeProductTypesProductsById(item.ProductId)
            };
        }

        public static OrderDetails CastDtoToOrderDetails(Dto_OrderDetails item)
        {
            return new OrderDetails()
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
            };
        }

        public static List<Dto_OrderDetails> CastOrderDetailsToDto(List<OrderDetails> items)
        {
            var result = new List<Dto_OrderDetails>();
            foreach (var item in items)
            {
                result.Add(new Dto_OrderDetails()
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId
                    //Order = orders.GetOrderById(item.OrderId),
                    //Product = prods.GetIncludeProductTypesProductsById(item.ProductId)
                });
            }
            return result;
        }

        public static List<OrderDetails> CastDtoToOrderDetails(List<Dto_OrderDetails> items)
        {
            var result = new List<OrderDetails>();
            foreach (var item in items)
            {
                result.Add(new OrderDetails()
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId
                });
            }
            return result;
        }
    }
}
