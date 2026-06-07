using System;

namespace TCG.Application.Dtos
{
    public class TournamentDto
    {
        public int TournamentId { get; set; }

        public int? TournamentLeague { get; set; }

        public string TournamentName { get; set; } = string.Empty;

        public string TournamentGame { get; set; } = string.Empty;

        public string TournamentFormat { get; set; } = string.Empty;

        public bool TournamentRequireDeck { get; set; }

        public int? TournamentRoundNum { get; set; }

        public int? TournamentMaxRoundNum { get; set; }

        public string TournamentDescription { get; set; } = string.Empty;

        public int TournamentMaxParticipants { get; set; }

        public string TournamentPairing { get; set; } = string.Empty;
        
        public bool TournamentSwissTopcut { get; set; }
        
        public int? TournamentSwissTopcutNum { get; set; }

        public DateOnly TournamentDate { get; set; }

        public TimeOnly TournamentTime { get; set; }

        public decimal TournamentEntryFee { get; set; }
    }
}