using System;
using System.Collections.Generic;
using System.Text;

namespace tcg.domain
{
    public partial class TourneyPlayer
    {
        public int TpId { get; set; }

        public int TpTourney { get; set; }

        public int TpPlayer { get; set; }
    }
}
