using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class Pairing
    {
        public int PairingId { get; set; }

        public int PairingTp1 { get; set; }

        public int PairingTp2 { get; set; }
    }
}
