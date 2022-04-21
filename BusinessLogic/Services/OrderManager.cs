using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;

namespace BusinessLogic.Services
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;
        public OrderManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddOrder(Dto_Order order)
        {
            _context.Orders.Add(OrderMapper.CastDtoToOrderModel(order));
            await _context.SaveChangesAsync();
        }

        public List<Dto_Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public int GetGeneralOrderCount()
        {
            return _context.Orders.ToList().Count();
        }

        public Dto_Order GetOrderById(int id)
        {
            var item = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return null;
            return OrderMapper.CastOrderModelToDto(item);
        }
    }
}
