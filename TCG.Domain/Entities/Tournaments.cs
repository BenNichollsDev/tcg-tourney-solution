//
// Program: Local Games Store Management System
// Filename: Tournaments.cs
// Author: Benjamin Nicholls
// Course: BSc Software Engineering (Hons)
// Module: CSY4022 - Computing Project Dissertation
// Module Leader: Amir Minai
// Supervisor: Mark Johnson
//
// Date: 14/06/2026
//
// Disclaimer: The following source code is the sole work of the author unless otherwise stated.
// Copyright (C) Benjamin Nicholls. All Rights Reserved.
//
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace TCG.Domain.Entities
{
    public partial class Tournament
    {
        [Column("tournament_id")]
        public int TournamentId { get; set; }

        [Column("tournament_seed")]
        public BigInteger TournamentSeed { get; set; }

        [Column("tournament_league")]
        public int? LeagueId { get; set; }

        [Column("tournament_name")]
        public string TournamentName { get; set; } = string.Empty;

        [Column("tournament_game")]
        public string TournamentGame { get; set; } = string.Empty;

        [Column("tournament_format")]
        public string TournamentFormat { get; set; } = string.Empty;

        [Column("tournament_require_deck")]
        public bool TournamentRequireDeck { get; set; }

        [Column("tournament_round_num")]
        public int? TournamentRoundNum { get; set; }

        [Column("tournament_max_round_num")]
        public int? TournamentMaxRoundNum { get; set; }

        [Column("tournament_description")]
        public string TournamentDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pairing type is required")]
        [Column("tournament_pairing")]
        public string TournamentPairing { get; set; } = string.Empty;

        [Column("tournament_date")]
        public DateOnly TournamentDate { get; set; }

        [Column("tournament_time")]
        public TimeOnly TournamentTime { get; set; }

        [Column("tournament_entry_fee")]
        public decimal TournamentEntryFee { get; set; }

        [Column("tournament_max_participants")]
        public int TournamentMaxParticipants { get; set; }

        [Column("tournament_swiss_topcut")]
        public bool TournamentSwissTopcut { get; set; }

        [Column("tournament_swiss_topcut_num")]
        public int TournamentSwissTopcutNum { get; set; }

        [Column("tournament_started")]
        public bool TournamentStarted { get; set; }

        [Column("tournament_round_in_progress")]
        public bool TournamentRoundInProgress { get; set; }

        [Column("tournament_finished")]
        public bool TournamentFinished { get; set; }

        [Column("tournament_cancelled")]
        public bool TournamentCancelled { get; set; }

        public League League { get; set; } = null!;

        public ICollection<TournamentPlayer> TournamentPlayers { get; set; } = new List<TournamentPlayer>();

        public ICollection<Pairing> Pairings { get; set; } = null!;
    }
}

