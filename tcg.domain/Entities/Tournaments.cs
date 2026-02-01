using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Domain.Entities
{
    public partial class Tournament
    {
        public int TournamentId { get; private set; }

        public int TournamentLeague { get; private set; }

        public string TournamentName { get; private set; } = string.Empty;

        public string TournamentGame { get; private set; } = string.Empty;

        public string TournamentFormat { get; private set; } = string.Empty;

        public bool TournamentRequireDeck { get; private set; }

        public int TournamentRoundNum { get; private set; }

        public string TournamentDescription { get; private set; } = string.Empty;

        public string TournamentPairing { get; private set; } = string.Empty;

        public DateTime TournamentCalendar { get; private set; }

        public decimal TournamentEntryFee { get; private set; }

        public int TournamentMaxParticipants { get; private set; }
    }

}
