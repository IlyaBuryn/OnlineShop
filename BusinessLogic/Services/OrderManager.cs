using AutoMapper;
using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;
        private MapperConfiguration _config;
        private Mapper _mapper;
        public OrderManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddOrder(Dto_Order order)
        {
            _config = GetConfigToDal();

            _mapper = new Mapper(_config);

            var tmp = _mapper.Map<Order>(order);
            _context.Orders.Add(tmp);
            await _context.SaveChangesAsync();
            return tmp.Id;
        }

        public async Task AddOrderDetails(Dto_OrderDetails orderDetails)
        {
            _config = GetConfigToDal();
            _mapper = new Mapper(_config);

            var tmp = _mapper.Map<OrderDetails>(orderDetails);
            await _context.OrderDetails.AddAsync(tmp);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Dto_Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dto_Order> GetFreeForDeliveryOrders()
        {
            _config = GetConfigToBll();

            _mapper = new Mapper(_config);

            var tmp = _context.Orders
               .Include(c => c.OrderDetails)
               .Include(c => c.DeliveryDetails)
               .Include(c => c.DeliveryDetails.DeliveryType)
               .Include(c => c.DeliveryDetails.PaymentType)
               .Include(c => c.DeliveryDetails.DeliveryStatus)
               .Include(c => c.OrderDetails).ThenInclude(p => p.Product)
               .Where(x => x.DeliveryDetails.IsBusyShipping == false && x.DeliveryDetails.DeliveryTypeId != 3)
               .AsNoTracking().ToList();
            var result = new List<Dto_Order>();
            foreach (var order in tmp)
                result.Add(_mapper.Map<Dto_Order>(order));
            return result;
        }

        public int GetGeneralOrderCount()
        {
            return _context.Orders.ToList().Count();
        }

        public Dto_Order GetOrderById(int id)
        {
            _config = GetConfigToBll();
            _mapper = new Mapper(_config);
            var item = _context.Orders
               .Include(c => c.OrderDetails)
               .Include(c => c.DeliveryDetails)
               .Include(c => c.DeliveryDetails.DeliveryType)
               .Include(c => c.DeliveryDetails.PaymentType)
               .Include(c => c.DeliveryDetails.DeliveryStatus)
               .Include(c => c.OrderDetails).ThenInclude(p => p.Product)
               .FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Dto_Order>(item);
        }

        public IEnumerable<Dto_Order> GetOrdersByEmail(string email)
        {
            _config = GetConfigToBll();

            _mapper = new Mapper(_config);

            var tmp = _context.Orders
               .Include(c => c.OrderDetails)
               .Include(c => c.DeliveryDetails)
               .Include(c => c.DeliveryDetails.DeliveryType)
               .Include(c => c.DeliveryDetails.PaymentType)
               .Include(c => c.DeliveryDetails.DeliveryStatus)
               .Include(c => c.OrderDetails).ThenInclude(p => p.Product)
               .Where(x => x.Email == email)
               .AsNoTracking().ToList();
            var result = new List<Dto_Order>();
            foreach (var order in tmp)
                result.Add(_mapper.Map<Dto_Order>(order));
            return result;
        }

        public async Task<int> UpdateOrder(Dto_Order order)
        {
            _config = GetConfigToDal();
            _mapper = new Mapper(_config);
            var item = _mapper.Map<Order>(order);

            var tmp = _context.Orders.FirstOrDefault(x => x.Id == item.Id);
            _context.Entry(tmp).CurrentValues.SetValues(item);
            _context.Entry(tmp).State = EntityState.Modified;
            _context.SaveChanges();
            return item.Id;
        }

        public async Task DeleteOrder(Dto_Order order)
        {
            var d = _context.Orders.FirstOrDefault(x => x.Id == order.Id);
            _context.Orders.Attach(d);
            _context.Orders.Remove(d);
            _context.SaveChanges();
        }

        private MapperConfiguration GetConfigToDal()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dto_Order, Order>();
                cfg.CreateMap<Dto_Product, Products>();
                cfg.CreateMap<Dto_OrderDetails, OrderDetails>();
                cfg.CreateMap<Dto_DeliveryDetails, DeliveryDetails>();
                cfg.CreateMap<Dto_DeliveryStatus, DeliveryStatus>();
                cfg.CreateMap<Dto_DeliveryType, DeliveryType>();
                cfg.CreateMap<Dto_PaymentType, PaymentType>();
            });
        }

        private MapperConfiguration GetConfigToBll()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, Dto_Order>();
                cfg.CreateMap<Products, Dto_Product>();
                cfg.CreateMap<OrderDetails, Dto_OrderDetails>();
                cfg.CreateMap<DeliveryDetails, Dto_DeliveryDetails>();
                cfg.CreateMap<DeliveryStatus, Dto_DeliveryStatus>();
                cfg.CreateMap<DeliveryType, Dto_DeliveryType>();
                cfg.CreateMap<DeliveryStatus, Dto_DeliveryStatus>();
                cfg.CreateMap<PaymentType, Dto_PaymentType>();
            });
        }

    }
}
