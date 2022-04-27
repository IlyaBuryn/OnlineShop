using BusinessLogic.DtoModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IApplicationUserManager
    {
        IEnumerable<IdentityUser> GetAllAppUsers();
        IdentityUser GetById(string id);
        int UpdateAppUser(IdentityUser applicationUser);
        int RemoveAppUser(IdentityUser applicationUser);
        int CreateAppUser(IdentityUser applicationUser);
        IdentityUser GetUserInfo(string inputEmail);
    }
}
