using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.DTOs
{
    public class StaffDTO
    {
        public int StaffId { get; set; }

        public string StaffFirstName { get; set; } = string.Empty;

        public string StaffSurname { get; set; } = string.Empty;

        public string StaffEmail { get; set; } = string.Empty;

        public long StaffMobile { get; set; }

        public bool StaffRoleManagement { get; set; }

        public bool StaffRoleHead { get; set; }
    }
}
