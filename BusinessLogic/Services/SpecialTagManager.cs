using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;

namespace BusinessLogic.Services
{
    public class SpecialTagManager : ISpecialTagManager
    {
        private readonly ApplicationDbContext _context;

        public SpecialTagManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSpecialTag(Dto_SpecialTag specialTag)
        {
            _context.SpecialTags.Add(SpecialTagMapper.CastDtoToSpecialTagModel(specialTag));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpecialTag(Dto_SpecialTag specialTag)
        {
            _context.SpecialTags.Remove(SpecialTagMapper.CastDtoToSpecialTagModel(specialTag));
            await _context.SaveChangesAsync();
        }

        public Dto_SpecialTag FindSpecialTagsById(int? id)
        {
            var item = _context.SpecialTags.Find(id);
            return SpecialTagMapper.CastSpecialTagModelToDto(item);
        }

        public List<Dto_SpecialTag> GetAllSpecialTags()
        {
            var items = _context.SpecialTags.ToList();
            return SpecialTagMapper.CastSpecialTagModelToDto(items);
        }

        public async Task UpdateSpecialTag(Dto_SpecialTag specialTag)
        {
            _context.SpecialTags.Update(SpecialTagMapper.CastDtoToSpecialTagModel(specialTag));
            await _context.SaveChangesAsync();
        }
    }
}
