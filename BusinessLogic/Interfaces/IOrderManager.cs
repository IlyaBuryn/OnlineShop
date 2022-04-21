using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface IOrderManager
    {
        Task AddOrder(Dto_Order order);
        List<Dto_Order> GetAllOrders();
        int GetGeneralOrderCount();
        Dto_Order GetOrderById(int id);
    }
}
