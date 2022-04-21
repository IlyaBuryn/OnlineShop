using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.DtoModels
{
    public class Dto_ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
