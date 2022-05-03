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
    public class PaymentManager : ICustomGenericService<DtoModels.Dto_PaymentType>
    {
        private readonly ApplicationDbContext _context;
        private MapperConfiguration _config;
        private Mapper _mapper;

        public PaymentManager(ApplicationDbContext context)
        {
            _context = context;
            _config = new MapperConfiguration(cfg => cfg.CreateMap<PaymentType, Dto_PaymentType>());
            _mapper = new Mapper(_config);
        }

        public int CreateObject(Dto_PaymentType? entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteObject(Dto_PaymentType? entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dto_PaymentType> GetAllObjects()
        {
            var types = _mapper.Map<IEnumerable<Dto_PaymentType>>(
                _context.PaymentTypes.AsNoTracking());
            return types;
        }

        public Dto_PaymentType GetObjectById(int? id)
        {
            var pay = _mapper.Map<Dto_PaymentType>(
                _context.PaymentTypes.AsNoTracking().FirstOrDefault(x => x.Id == id));
            return pay;
        }

        public int UpdateObject(Dto_PaymentType? entity)
        {
            throw new NotImplementedException();
        }
    }
}
