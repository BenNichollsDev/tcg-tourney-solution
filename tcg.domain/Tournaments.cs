using System;
using System.Collections.Generic;
using System.Text;

namespace tcg.domain
{
    public partial class Tournament
    {
        public int TourneyId { get; set; }

        public int TourneyLeague { get; set; }

        public string TourneyName { get; set; } = string.Empty;

        public string TourneyGame { get; set; } = string.Empty;

        public string TourneyFormat { get; set; } = string.Empty;

        public bool TourneyRequireDeck { get; set; }

        public int TourneyRoundNum { get; set; }

        public string TourneyDescription { get; set; } = string.Empty;

        public string TourneyPairing { get; set; } = string.Empty;

        public DateTime TourneyCalendar { get; set; }

        public decimal TourneyEntryFee { get; set; }

        public int TourneyMaxParticipants { get; set; }
    }

}
