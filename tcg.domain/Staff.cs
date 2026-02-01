using System;
using System.Collections.Generic;
using System.Text;

namespace tcg.domain
{
    public partial class Staff
    {
        public int StaffId { get; set; }

        public string StaffFirstName { get; set; } = string.Empty;

        public string StaffSurname { get; set; } = string.Empty;

        public string StaffEmail { get; set; } = string.Empty;

        public long StaffMobile { get; set; }
    }
}
