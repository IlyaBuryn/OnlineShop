using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DtoModels
{
    public class Dto_Vm_RoleUserVm
    {
        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
    }
}
