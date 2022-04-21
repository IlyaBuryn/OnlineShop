using BusinessLogic.DtoModels;
using DataAccessLayer.Models;

namespace BusinessLogic.Maps
{
    public class UserRoleMapper
    {
        public static List<Dto_ApplicationUser> CastAppUserModelToDto(List<ApplicationUser> items)
        {
            var result = new List<Dto_ApplicationUser>();
            foreach (var item in items)
            {
                result.Add(new Dto_ApplicationUser()
                {
                    AccessFailedCount = item.AccessFailedCount,
                    ConcurrencyStamp = item.ConcurrencyStamp,
                    Email = item.Email,
                    EmailConfirmed = item.EmailConfirmed,
                    FirstName = item.FirstName,
                    Id = item.Id,
                    LastName = item.LastName,
                    LockoutEnabled = item.LockoutEnabled,
                    LockoutEnd = item.LockoutEnd,
                    NormalizedEmail = item.NormalizedEmail,
                    NormalizedUserName = item.NormalizedUserName,
                    PasswordHash = item.PasswordHash,
                    PhoneNumber = item.PhoneNumber,
                    PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                    SecurityStamp = item.SecurityStamp,
                    TwoFactorEnabled = item.TwoFactorEnabled,
                    UserName = item.UserName
                });
            }
            return result;
        }

        public static Dto_ApplicationUser CastAppUserModelToDto(ApplicationUser item)
        {
            return new Dto_ApplicationUser()
            {
                AccessFailedCount = item.AccessFailedCount,
                ConcurrencyStamp = item.ConcurrencyStamp,
                Email = item.Email,
                EmailConfirmed = item.EmailConfirmed,
                FirstName = item.FirstName,
                Id = item.Id,
                LastName = item.LastName,
                LockoutEnabled = item.LockoutEnabled,
                LockoutEnd = item.LockoutEnd,
                NormalizedEmail = item.NormalizedEmail,
                NormalizedUserName = item.NormalizedUserName,
                PasswordHash = item.PasswordHash,
                PhoneNumber = item.PhoneNumber,
                PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                SecurityStamp = item.SecurityStamp,
                TwoFactorEnabled = item.TwoFactorEnabled,
                UserName = item.UserName
            };
        }

        public static ApplicationUser CastDtoToAppUserModel(Dto_ApplicationUser item)
        {
            return new ApplicationUser()
            {
                AccessFailedCount = item.AccessFailedCount,
                ConcurrencyStamp = item.ConcurrencyStamp,
                Email = item.Email,
                EmailConfirmed = item.EmailConfirmed,
                FirstName = item.FirstName,
                Id = item.Id,
                LastName = item.LastName,
                LockoutEnabled = item.LockoutEnabled,
                LockoutEnd = item.LockoutEnd,
                NormalizedEmail = item.NormalizedEmail,
                NormalizedUserName = item.NormalizedUserName,
                PasswordHash = item.PasswordHash,
                PhoneNumber = item.PhoneNumber,
                PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                SecurityStamp = item.SecurityStamp,
                TwoFactorEnabled = item.TwoFactorEnabled,
                UserName = item.UserName
            };
        }
    }
}
