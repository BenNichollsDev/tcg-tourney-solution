/*
Program: Local Games Store Management System
Filename: TournamentDTO.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace TCG.Application.Dtos
{
    public class TournamentDto
    {
        public int TournamentId { get; set; }

        public BigInteger TournamentSeed { get; set; } 

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

        public bool TournamentStarted { get; set; }

        public bool TournamentRoundInProgress { get; set; }

        public bool TournamentFinished { get; set; }

        public bool TournamentCancelled { get; set; }
    }
}
