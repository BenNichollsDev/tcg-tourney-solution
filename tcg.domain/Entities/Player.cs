using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class Player
    {
        public int PlayerId { get; private set; }

        public string PlayerFirstName { get; private set; } = string.Empty;

        public string PlayerSurname { get; private set; } = string.Empty;

        public DateTime PlayerDob { get; private set; }

        public string PlayerEmail { get; private set; } = string.Empty;

        public long PlayerMobile { get; private set; }
    }
}
