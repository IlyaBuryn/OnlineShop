using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface IOrderManager
    {
        Task<int> AddOrder(Dto_Order order);
        IEnumerable<Dto_Order> GetAllOrders();
        IEnumerable<Dto_Order> GetFreeForDeliveryOrders();
        int GetGeneralOrderCount();
        Dto_Order GetOrderById(int id);
        Task AddOrderDetails(Dto_OrderDetails orderDetails);
        Task<int> UpdateOrder(Dto_Order order);
    }
}
