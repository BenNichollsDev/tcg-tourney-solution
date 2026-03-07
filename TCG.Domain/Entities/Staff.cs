using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Staff
    {
        [Column("staff_id")]
        public int StaffId { get; private set; }

        [Column("staff_first_name")]
        public string StaffFirstName { get; private set; } = string.Empty;

        [Column("staff_surname")]
        public string StaffSurname { get; private set; } = string.Empty;

        [Column("staff_email")]
        public string StaffEmail { get; private set; } = string.Empty;

        [Column("staff_password")]
        public string StaffPassword { get; private set; } = string.Empty;

        [Column("staff_mobile")]
        public string StaffMobile { get; private set; } = string.Empty;

        [Column("staff_role_management")]
        public bool StaffRoleManagement { get; private set; }

        [Column("staff_role_head")]
        public bool StaffRoleHead { get; private set; }
    }
}