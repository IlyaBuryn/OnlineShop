using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ProductManager : IProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Dto_Product product)
        {
            _context.Products.Add(ProductMapper.CastDtoToProductModel(product));
            await _context.SaveChangesAsync();
        }

        public Dto_Product GetFullProductById(int? id)
        {
            var item = _context.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            return ProductMapper.CastProductModelToDto(item);
        }

        public IEnumerable<Dto_Product> GetFullProducts()
        {
            var items = _context.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTag).ToList();
            return ProductMapper.CastProductModelToDto(items);
        }

        public IEnumerable<Dto_Product> GetProductsBetweenPrice(decimal? lowAmount, decimal? hightAmount)
        {
            var items = _context.Products.Include(c => c.ProductTypes)
                .Include(c => c.SpecialTag)
                .Where(c => c.Price >= lowAmount && c.Price <= hightAmount).ToList();
            return ProductMapper.CastProductModelToDto(items);
        }

        public Dto_Product GetProductByName(string name)
        {
            var item = _context.Products.FirstOrDefault(c => c.Name == name);
            return ProductMapper.CastProductModelToDto(item);
        }

        public async Task RemoveProduct(Dto_Product product)
        {

            _context.Products.Remove(ProductMapper.CastDtoToProductModel(product));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Dto_Product product)
        {
            _context.Products.Update(ProductMapper.CastDtoToProductModel(product));
            await _context.SaveChangesAsync();
        }

        public Dto_Product GetProductByIdWithoutTags(int? id)
        {
            var item = _context.Products.FirstOrDefault(c => c.Id == id);
            return ProductMapper.CastProductModelToDto(item);
        }

        public Dto_Product GetIncludeProductTypesProductsById(int? id)
        {
            var item = _context.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            return ProductMapper.CastProductModelToDto(item);
        }

        public IEnumerable<Dto_Product> FindFullProductsThatContainName(string searchString)
        {
            var items = _context.Products
                .Include(c => c.ProductTypes)
                .Include(c => c.SpecialTag)
                .Where(c => c.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return ProductMapper.CastProductModelToDto(items);
        }

        public IEnumerable<Dto_Product> GetProductsByProductType(int productTypeId)
        {
            var items = _context.Products
                .Include(c => c.ProductTypes)
                .Include(c => c.SpecialTag)
                .Where(c => c.ProductTypeId == productTypeId).ToList();
            return ProductMapper.CastProductModelToDto(items);
        }
    }
}
