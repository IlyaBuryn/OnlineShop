using AutoMapper;
using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class DeliveryTypeManager : ICustomGenericService<Dto_DeliveryType>
    {
        private readonly ApplicationDbContext _context;
        private MapperConfiguration _config;
        private Mapper _mapper;

        public DeliveryTypeManager(ApplicationDbContext context)
        {
            _context = context;
            _config = new MapperConfiguration(cfg => cfg.CreateMap<DeliveryType, Dto_DeliveryType>());
            _mapper = new Mapper(_config);
        }

        public int CreateObject(Dto_DeliveryType? entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteObject(Dto_DeliveryType? entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dto_DeliveryType> GetAllObjects()
        {
            var types = _mapper.Map<IEnumerable<Dto_DeliveryType>>(
                _context.DeliveryTypes.AsNoTracking());
            return types;
        }

        public Dto_DeliveryType GetObjectById(int? id)
        {
            var type = _mapper.Map<Dto_DeliveryType>(
                _context.DeliveryTypes.AsNoTracking().FirstOrDefault(x => x.Id == id));
            return type;
        }

        public int UpdateObject(Dto_DeliveryType? entity)
        {
            throw new NotImplementedException();
        }
    }
}
