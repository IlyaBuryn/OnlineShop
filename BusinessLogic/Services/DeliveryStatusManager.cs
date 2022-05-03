using AutoMapper;
using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class DeliveryStatusManager : ICustomGenericServiceAsyncMembers<Dto_DeliveryStatus>
    {
        private readonly ApplicationDbContext _context;
        private MapperConfiguration _config;
        private Mapper _mapper;

        public DeliveryStatusManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateObjectAsync(Dto_DeliveryStatus? entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteObjectAsync(Dto_DeliveryStatus? entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dto_DeliveryStatus>> GetAllObjectsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Dto_DeliveryStatus> GetObjectByIdAsync(int? id)
        {
            _config = new MapperConfiguration(cfg => cfg.CreateMap<DeliveryStatus, Dto_DeliveryStatus>());
            _mapper = new Mapper(_config);
            var tmp = _context.DeliveryStatus.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Dto_DeliveryStatus>(tmp);
        }

        public async Task<Dto_DeliveryStatus> GetObjectByNameAsync(string name)
        {
            _config = new MapperConfiguration(cfg => cfg.CreateMap<DeliveryStatus, Dto_DeliveryStatus>());
            _mapper = new Mapper(_config);
            var tmp = _context.DeliveryStatus.AsNoTracking().FirstOrDefault(x => x.Name == name);
            return _mapper.Map<Dto_DeliveryStatus>(tmp);
        }

        public Task<int> UpdateObjectAsync(Dto_DeliveryStatus? entity)
        {
            throw new NotImplementedException();
        }
    }

}
