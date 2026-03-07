using System;
using TCG.Application.Interfaces;

namespace TCG.Application.Dtos
{
    public class StaffDto : IHasId
    {
        public int StaffId { get; set; }
        
        int IHasId.Id => StaffId;

        public string StaffFirstName { get; set; } = string.Empty;

        public string StaffSurname { get; set; } = string.Empty;

        public string StaffEmail { get; set; } = string.Empty;

        public string StaffPassword { get; set; } = string.Empty;

        public string StaffMobile { get; set; } = string.Empty;

        public bool StaffRoleManagement { get; set; }

        public bool StaffRoleHead { get; set; }
    }
}