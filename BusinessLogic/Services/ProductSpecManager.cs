using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductSpecManager : IProductSpecManager
    {
        private readonly IDescriptionRepository repository;

        public ProductSpecManager(IDescriptionRepository _ropository)
        {
            repository = _ropository;
        }

        public async Task<string[]> GetProductDescription(Dto_Product product)
        {
            var item = await repository.GetProductDescByProduct(
                ProductMapper.CastDtoToProductModel(product));
            if (item != null)
            {
                var expand = item.ProductCharacteristics;
                return item.ProductDescription.Split('|');
            }
            return null;
        }

        public async Task<Dictionary<string, Dictionary<string, string>>> GetProductSpecs(Dto_Product product)
        {
            var item = await repository.GetProductDescByProduct(
                ProductMapper.CastDtoToProductModel(product));
            if (item != null)
            {
                var expand = item.ProductCharacteristics;
                return Splitter.ConvertExpandObjectToDictionary(expand);
            }
            return null;
        }

        public async Task<List<string>> GetProductColors(Dto_Product product)
        {
            var item = await repository.GetProductDescByProduct(
                ProductMapper.CastDtoToProductModel(product));
            if (item != null)
            {
                var expand = item?.Colors;
                return Splitter.ConvertEcpandObjectToStringList(expand);
            }
            return null;
        }
    }
}
