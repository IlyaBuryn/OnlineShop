using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Maps;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;

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
                         join a in _context.Users on ur.UserId equals a.Id
                         select new Dto_Vm_UserRoleMapping()
                         {
                             UserId = ur.UserId,
                             RoleId = ur.RoleId,
                             UserName = a.UserName,
                             RoleName = r.Name
                         };
            return result;
        }

        public IdentityUser GetUserById(string id)
        {
            var item = _context.Users.FirstOrDefault(x => x.Id == id);
            return item;
        }

        public Dto_Vm_SessionUserVm GetUsersForLogin(IdentityUser userInfo, string inputEmail)
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

        public List<IdentityUser> GetUsersIdWithLockout()
        {
            var items = _context.Users.Where(x => x.LockoutEnd < System.DateTime.Now || x.LockoutEnd == null).ToList();
            return items;
            return items;
        }
    }
}
