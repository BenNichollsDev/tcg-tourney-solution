using System;
using System.Collections.Generic;
using System.Text;

namespace TCG.Application.DTOs
{
    public class TournamentFormDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Game { get; set; } = string.Empty;

        public string Format { get; set; } = string.Empty;

        public bool RequireDecklist { get; set; }

        public string PairingFormat { get; set; } = "Swiss";

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }
    }
}
