namespace TCG.Application.Dtos
{
    public class PairingDto
    {
        public int PairingId { get; set; }

        public int TournamentId { get; set; }

        public int? RoundNumber { get; set; }

        public int Player1Id { get; set; }

        public int? Player2Id { get; set; }

        public int? Player1Score { get; set; }

        public int? Player2Score { get; set; }

        public int? WinnerId { get; set; }

        public int? Player1GameCount { get; set; }

        public int? Player2GameCount { get; set; }

        public bool HasResult { get; set; }

        public bool PairingDraw { get; set; }
    }
}
