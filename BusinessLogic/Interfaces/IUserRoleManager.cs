using BusinessLogic.DtoModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Interfaces
{
    public interface IUserRoleManager
    {
        List<IdentityUser> GetUsersIdWithLockout();
        IdentityUser GetUserById(string id);
        IQueryable<Dto_Vm_UserRoleMapping> GetAssignUsers();
        Dto_Vm_SessionUserVm GetUsersForLogin(IdentityUser userInfo, string inputEmail);
    }
}
