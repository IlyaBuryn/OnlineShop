using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;

namespace BusinessLogic.Services
{
    public class AppUserManager : IApplicationUserManager
    {
        private readonly ApplicationDbContext _context;
        public AppUserManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Dto_ApplicationUser> GetAllAppUsers()
        {
            var items = _context.ApplicationUsers.ToList();
            return UserRoleMapper.CastAppUserModelToDto(items);
        }

        public Dto_ApplicationUser GetById(string id)
        {
            var item = _context.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            return UserRoleMapper.CastAppUserModelToDto(item);
        }

        public int UpdateAppUser(Dto_ApplicationUser applicationUser)
        {
            _context.ApplicationUsers.Update(UserRoleMapper.CastDtoToAppUserModel(applicationUser));
            return _context.SaveChanges();
        }

        public int RemoveAppUser(Dto_ApplicationUser applicationUser)
        {
            _context.ApplicationUsers.Remove(UserRoleMapper.CastDtoToAppUserModel(applicationUser));
            return _context.SaveChanges();
        }

        public Dto_ApplicationUser GetUserInfo(string inputEmail)
        {
            var item = _context.ApplicationUsers.FirstOrDefault(c => c.UserName.ToLower() == inputEmail.ToLower());
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
    }
}
