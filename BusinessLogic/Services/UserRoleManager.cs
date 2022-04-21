using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;

namespace BusinessLogic.Services
{
    public class UserRoleManager : IUserRoleManager
    {
        private readonly ApplicationDbContext _context;
        public UserRoleManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Dto_Vm_UserRoleMapping> GetAssignUsers()
        {
            var result = from ur in _context.UserRoles
                         join r in _context.Roles on ur.RoleId equals r.Id
                         join a in _context.ApplicationUsers on ur.UserId equals a.Id
                         select new Dto_Vm_UserRoleMapping()
                         {
                             UserId = ur.UserId,
                             RoleId = ur.RoleId,
                             UserName = a.UserName,
                             RoleName = r.Name
                         };
            return result;
        }

        public Dto_ApplicationUser GetUserById(string id)
        {
            var item = _context.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            return UserRoleMapper.CastAppUserModelToDto(item);
        }

        public Dto_Vm_SessionUserVm GetUsersForLogin(Dto_ApplicationUser userInfo, string inputEmail)
        {
            return (from ur in _context.UserRoles
                    join r in _context.Roles on ur.RoleId equals r.Id
                    where ur.UserId == userInfo.Id
                    select new Dto_Vm_SessionUserVm()
                    {
                        UserName = inputEmail,
                        RoleName = r.Name
                    }).FirstOrDefault();
        }

        public List<Dto_ApplicationUser> GetUsersIdWithLockout()
        {
            var items = _context.ApplicationUsers.Where(x => x.LockoutEnd < System.DateTime.Now || x.LockoutEnd == null).ToList();
            return UserRoleMapper.CastAppUserModelToDto(items);
        }
    }
}
