using System.ComponentModel.DataAnnotations;
using TCG.Application.Interfaces;

namespace TCG.Application.Dtos
{
    public class StaffDto : IHasId
    {
        public int StaffId { get; set; }
        
        int IHasId.Id => StaffId;

        [Required(ErrorMessage = "First name is required.")]
        public string StaffFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required.")]
        public string StaffSurname { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address.")]
        public string StaffEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string StaffPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\+?[1-9]\d{9,14}$", ErrorMessage = "Invalid mobile number.")]
        public string StaffMobile { get; set; } = string.Empty;

        public bool StaffRoleManagement { get; set; }

        public bool StaffRoleHead { get; set; }
    }
}