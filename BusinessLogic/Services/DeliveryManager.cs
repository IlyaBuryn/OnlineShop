using AutoMapper;
using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class DeliveryManager : ICustomGenericServiceAsync<Dto_DeliveryDetails>
    {
        private readonly ApplicationDbContext _context;
        private MapperConfiguration _config;
        private Mapper _mapper;

        public DeliveryManager(ApplicationDbContext context)
        {
            _context = context;
            _config = new MapperConfiguration(cfg => cfg.CreateMap<Dto_DeliveryDetails, DeliveryDetails>()
                .ForMember(dto => dto.PaymentType, opt => opt.Ignore())
                .ForMember(dto => dto.DeliveryType, opt => opt.Ignore())
                .ReverseMap());
            _mapper = new Mapper(_config);
        }

        public async Task<int> CreateObjectAsync(Dto_DeliveryDetails? entity)
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dto_DeliveryDetails, DeliveryDetails>();
            });
            _mapper = new Mapper(_config);
            var obj = _mapper.Map<DeliveryDetails>(entity);
            await _context.DeliveryDetails.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj.Id;
        }

        public Task<int> DeleteObjectAsync(Dto_DeliveryDetails? entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dto_DeliveryDetails>> GetAllObjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Dto_DeliveryDetails> GetObjectByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateObjectAsync(Dto_DeliveryDetails? entity)
        {
            throw new NotImplementedException();
        }
    }
}
