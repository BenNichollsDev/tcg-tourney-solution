using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Pairing
    {
        [Column("pairing_id")]
        public int PairingId { get; private set; }

        [Column("pairing_tp1")]
        public int PairingTp1 { get; private set; }

        [Column("pairing_tp2")]
        public int PairingTp2 { get; private set; }
    }
}
