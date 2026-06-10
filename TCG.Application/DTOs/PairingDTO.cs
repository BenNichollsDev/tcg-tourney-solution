namespace TCG.Application.Dtos
{
    public class PairingDto
    {
        public int? PairingId { get; set; }

        public int? PairingTournamentId { get; set; }

        public int? PairingTp1 { get; set; }

        public int? PairingTp2 { get; set; }

        // Scores and game counts
        public int? PairingPlayer1Score { get; set; }

        public int? PairingPlayer2Score { get; set; }

        public int? PairingPlayer1GameCount { get; set; }

        public int? PairingPlayer2GameCount { get; set; }

        public bool PairingHasResult { get; set; }

        public int RoundNumber { get; set; }
    }
}