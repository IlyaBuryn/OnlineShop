using BusinessLogic.Interfaces;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class AppUserManager : IApplicationUserManager
    {
        private readonly ApplicationDbContext _context;

        public AppUserManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IdentityUser> GetAllAppUsers()
        {
            var users = _context.Users.AsNoTracking().ToList();
            return users;
        }

        public IdentityUser GetById(string id)
        {
            var item = _context.Users.AsNoTracking().FirstOrDefault(c => c.Id == id);
            return item;
        }

        public int UpdateAppUser(IdentityUser applicationUser)
        {
            _context.Users.Update(applicationUser);
            return _context.SaveChanges();
        }

        public int CreateAppUser(IdentityUser applicationUser)
        {
            _context.Users.Add(applicationUser);
            return _context.SaveChanges();
        }

        public int RemoveAppUser(IdentityUser applicationUser)
        {
            _context.Users.Remove(applicationUser);
            return _context.SaveChanges();
        }

        public IdentityUser GetUserInfo(string inputEmail)
        {
            var item = _context.Users.AsNoTracking().FirstOrDefault(c => c.UserName.ToLower() == inputEmail.ToLower());
            return item;
        }
    }
}
