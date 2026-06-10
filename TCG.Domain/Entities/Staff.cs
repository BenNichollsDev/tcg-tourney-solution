    using System.ComponentModel.DataAnnotations.Schema;

    namespace TCG.Domain.Entities
    {
        public partial class Staff
        {
            [Column("staff_id")]
            public int StaffId { get; set; }

            [Column("staff_first_name")]
            public string StaffFirstName { get; set; } = string.Empty;

            [Column("staff_surname")]
            public string StaffSurname { get; set; } = string.Empty;

            [Column("staff_email")]
            public string StaffEmail { get; set; } = string.Empty;

            [Column("staff_password")]
            public string StaffPassword { get; set; } = string.Empty;

            [Column("staff_mobile")]
            public string StaffMobile { get; set; } = string.Empty;

            [Column("staff_role_management")]
            public bool StaffRoleManagement { get; set; }

            [Column("staff_role_head")]
            public bool StaffRoleHead { get; set; }
        }
    }