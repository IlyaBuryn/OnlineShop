using BusinessLogic.DtoModels;
using DataAccessLayer.Models;

namespace BusinessLogic.Maps
{
    public class SpecialTagMapper
    {
        public static List<Dto_SpecialTag> CastSpecialTagModelToDto(List<SpecialTag> items)
        {
            var result = new List<Dto_SpecialTag>();
            foreach (var item in items)
            {
                result.Add(new Dto_SpecialTag
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return result;
        }

        public static Dto_SpecialTag CastSpecialTagModelToDto(SpecialTag item)
        {
            return new Dto_SpecialTag()
            {
                Id = item.Id,
                Name = item.Name,
            };
        }

        public static SpecialTag CastDtoToSpecialTagModel(Dto_SpecialTag item)
        {
            return new SpecialTag()
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}
