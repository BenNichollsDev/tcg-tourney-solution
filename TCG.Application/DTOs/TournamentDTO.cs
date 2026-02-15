using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.Dtos
{
    public class TournamentDto
    {
        public int TournamentId { get; set; }

        public int TournamentLeague { get; set; }

        public string TournamentName { get; set; } = string.Empty;

        public string TournamentGame { get; set; } = string.Empty;

        public string TournamentFormat { get; set; } = string.Empty;

        public bool TournamentRequireDeck { get; set; }

        public int TournamentRoundNum { get; set; }

        public string TournamentDescription { get; set; } = string.Empty;

        public string TournamentPairing { get; set; } = string.Empty;

        public DateOnly TournamentDate { get; set; }

        public TimeOnly TournamentTime { get; set; }

        public decimal TournamentEntryFee { get; set; }

        public int TournamentMaxParticipants { get; set; }
    }
}
