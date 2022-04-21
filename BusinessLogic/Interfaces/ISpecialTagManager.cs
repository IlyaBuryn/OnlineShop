using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface ISpecialTagManager
    {
        List<Dto_SpecialTag> GetAllSpecialTags();
        Task AddSpecialTag(Dto_SpecialTag specialTag);
        Task UpdateSpecialTag(Dto_SpecialTag specialTag);
        Task DeleteSpecialTag(Dto_SpecialTag specialTag);
        Dto_SpecialTag FindSpecialTagsById(int? id);
    }
}
