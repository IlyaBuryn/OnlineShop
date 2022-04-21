using BusinessLogic.DtoModels;
using DataAccessLayer.Models;

namespace BusinessLogic.Maps
{
    public class OrderMapper
    {
        public static Dto_Order CastOrderModelToDto(Order item)
        {
            return new Dto_Order()
            {
                Address = item.Address,
                Email = item.Email,
                Id = item.Id,
                Name = item.Name,
                OrderDate = item.OrderDate,      
                OrderNo = item.OrderNo,
                PhoneNo = item.PhoneNo
            };
        }

        public static Order CastDtoToOrderModel(Dto_Order item)
        {
            return new Order()
            {
                Address = item.Address,
                Email = item.Email,
                Id = item.Id,
                Name = item.Name,
                OrderDate = item.OrderDate,
                OrderNo = item.OrderNo,
                PhoneNo = item.PhoneNo,
            };
        }
    }
}
