//
// Program: Local Games Store Management System
// Filename: League.cs
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
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class League
    {
        [Column("league_id")]
        public int LeagueId { get; set; }

        [Column("league_name")]
        public string LeagueName { get; set; } = string.Empty;

        [Column("league_game")]
        public string LeagueGame { get; set; } = string.Empty;

        [Column("league_description")]
        public string LeagueDescription { get; set; } = string.Empty;

        public ICollection<Tournament> Tournaments { get; set; }
            = new List<Tournament>();
    }
}

