using System;

namespace TCG.Application.Dtos
{
    public class PairingDto
    {
        public int PairingId { get; set; }

        public int? PairingTp1 { get; set; }

        public int? PairingTp2 { get; set; }
        
        public int? PairingTp1Score { get; set; }
        
        public int? PairingTp2Score { get; set; }

        public string? PairingWinner { get; set; }
    }
}