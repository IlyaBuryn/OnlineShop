using BusinessLogic.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IApplicationUserManager
    {
        List<Dto_ApplicationUser> GetAllAppUsers();
        Dto_ApplicationUser GetById(string id);
        int UpdateAppUser(Dto_ApplicationUser applicationUser);
        int RemoveAppUser(Dto_ApplicationUser applicationUser);
        Dto_ApplicationUser GetUserInfo(string inputEmail);
    }
}
