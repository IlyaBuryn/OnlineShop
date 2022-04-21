using BusinessLogic.DtoModels;

namespace BusinessLogic.Interfaces
{
    public interface IUserRoleManager
    {
        List<Dto_ApplicationUser> GetUsersIdWithLockout();
        Dto_ApplicationUser GetUserById(string id);
        IQueryable<Dto_Vm_UserRoleMapping> GetAssignUsers();
        Dto_Vm_SessionUserVm GetUsersForLogin(Dto_ApplicationUser userInfo, string inputEmail);
    }
}
