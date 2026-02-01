using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class Staff
    {
        public int StaffId { get; private set; }

        public string StaffFirstName { get; private set; } = string.Empty;

        public string StaffSurname { get; private set; } = string.Empty;

        public string StaffEmail { get; private set; } = string.Empty;

        public long StaffMobile { get; private set; }

        public bool StaffRoleManagement { get; private set; }

        public bool StaffRoleHead { get; private set; }

    }
}
