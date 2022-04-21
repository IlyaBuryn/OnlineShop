using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ProductTypesManager : IProductTypesManager
    {
        private readonly ApplicationDbContext _context;

        public ProductTypesManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductType(Dto_ProductType productType)
        {
            _context.ProductTypes.Add(ProductTypesMapper.CastDtoToProductTypeModel(productType));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductType(Dto_ProductType productType)
        {
            _context.ProductTypes.Remove(ProductTypesMapper.CastDtoToProductTypeModel(productType));
            await _context.SaveChangesAsync();
        }

        public Dto_ProductType FindProductType(int? id)
        {
            var item = _context.ProductTypes.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return ProductTypesMapper.CastProductTypeModelToDto(item);
        }

        public List<Dto_ProductType> GetAllProductTypes()
        {
            var items = _context.ProductTypes.AsNoTracking().ToList();
            return ProductTypesMapper.CastProductTypeModelToDto(items);
        }

        public IEnumerable<Dto_ProductType> GetFewRandomCategories(int count = 5)
        {
            var items = _context.ProductTypes.AsNoTracking().ToList();

            Random rnd = new Random();
            return ProductTypesMapper.CastProductTypeModelToDto(items.OrderBy(x => rnd.Next()).Take(count).ToList());
        }

        public async Task UpdateProductType(Dto_ProductType productType)
        {
            _context.ProductTypes.Update(ProductTypesMapper.CastDtoToProductTypeModel(productType));
            await _context.SaveChangesAsync();
        }
    }
}
