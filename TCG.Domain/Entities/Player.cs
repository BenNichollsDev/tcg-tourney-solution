/*
Program: Local Games Store Management System
Filename: Player.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations.Schema;

namespace TCG.Domain.Entities
{
    public partial class Player
    {
        [Column("player_id")]
        public int PlayerId { get; set; }

        [Column("player_first_name")]
        public string PlayerFirstName { get; set; } = string.Empty;

        [Column("player_last_name")]
        public string PlayerLastName { get; set; } = string.Empty;

        [Column("player_email")]
        public string PlayerEmail { get; set; } = string.Empty;

        [Column("player_phone")]
        public string PlayerPhone { get; set; } = string.Empty;

        [Column("player_dob")]
        public DateOnly PlayerDOB { get; set; }

        [Column("player_gender")]
        public string PlayerGender { get; set; } = string.Empty;

        [Column("player_password")]
        public string PlayerPassword { get; set; } = string.Empty;

        public ICollection<TournamentPlayer>? TournamentPlayers { get; set; } = new List<TournamentPlayer>();
    }
}

