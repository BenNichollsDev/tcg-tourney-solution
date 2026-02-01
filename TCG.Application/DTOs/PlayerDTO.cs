using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.Dtos
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }

        public string PlayerFirstName { get; set; } = string.Empty;

        public string PlayerSurname { get; set; } = string.Empty;

        public DateTime PlayerDob { get; set; }

        public string PlayerEmail { get; set; } = string.Empty;

        public long PlayerMobile { get; set; }
    }
}
