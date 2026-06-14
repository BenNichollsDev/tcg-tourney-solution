/*
Program: Local Games Store Management System
Filename: TournamentScoringService_UPDATE.cs
Author: Benjamin Nicholls
Course: BSc Software Engineering (Hons)
Module: CSY4022 - Computing Project Dissertation
Module Leader: Amir Minai
Supervisor: Mark Johnson

Date: 14/06/2026

Disclaimer: The following source code is the sole work of the author unless otherwise stated.
Copyright (C) Benjamin Nicholls. All Rights Reserved.
*/
            // Flatten all players from the standings dictionary list into a single working list
            var allPlayers = new List<ITournamentScoringService.PlayerComputedStats>();
            foreach (var standingDict in standings)
            {
                allPlayers.AddRange(standingDict.Values);
            }

            // Get the tournament to check if it's finished and what game type it is
            var tournament = await _tService.GetByIdAsync(tournamentId);

            // If the game is MTG, sort by MTG tiebreaker rules
            if (tournamentGame == "mtg")
            {
                // Sort players in descending order by MTG tiebreaker rules
                // Sort by: MatchPoints > OpMatchWinPercent > GameWinPercent > OpGameWinPercent
                // Disqualified players do not get rankings
                var activePlayers = allPlayers.Where(p => !p.IsDisqualified).OrderByDescending(p => p.MatchPoints)
                    .ThenByDescending(p => p.OpMatchWinPercent)
                    .ThenByDescending(p => p.GameWinPercent)
                    .ThenByDescending(p => p.OpGameWinPercent)
                    .ToList();

                // Assign positions, giving the same position to players with identical tiebreaker values
                int currentPosition = 1;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    // Check if this player has the same tiebreaker stats as the previous player
                    if (i > 0 && 
                        activePlayers[i].MatchPoints == activePlayers[i - 1].MatchPoints &&
                        Math.Abs(activePlayers[i].OpMatchWinPercent - activePlayers[i - 1].OpMatchWinPercent) < 0.001 &&
                        Math.Abs(activePlayers[i].GameWinPercent - activePlayers[i - 1].GameWinPercent) < 0.001 &&
                        Math.Abs(activePlayers[i].OpGameWinPercent - activePlayers[i - 1].OpGameWinPercent) < 0.001)
                    {
                        // Same position as previous player since all tiebreakers are equal
                        activePlayers[i].Position = activePlayers[i - 1].Position;
                    }
                    else
                    {
                        // New position starting from where we are in the list
                        currentPosition = i + 1;
                        activePlayers[i].Position = currentPosition;
                    }
                }

                // Disqualified players get no position (stays 0)
                foreach (var disqualifiedPlayer in allPlayers.Where(p => p.IsDisqualified))
                {
                    disqualifiedPlayer.Position = 0;
                }
            }
            // If the game is PKMN, sort by PKMN tiebreaker rules
            else if (tournamentGame == "pkmn")
            {
                // Sort players in descending order by PKMN tiebreaker rules
                // Sort by: OpMatchWinPercent > OpOpMatchWinPercent > HeadToHeadPosition
                var activePlayers = allPlayers.Where(p => !p.IsDisqualified).OrderByDescending(p => p.OpMatchWinPercent)
                    .ThenByDescending(p => p.OpOpMatchWinPercent)
                    .ThenBy(p => p.HeadToHeadPosition)
                    .ToList();

                // Assign positions, giving the same position to players with identical tiebreaker values
                int currentPosition = 1;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    // Check if this player has the same tiebreaker stats as the previous player
                    if (i > 0 && 
                        Math.Abs(activePlayers[i].OpMatchWinPercent - activePlayers[i - 1].OpMatchWinPercent) < 0.001 &&
                        Math.Abs(activePlayers[i].OpOpMatchWinPercent - activePlayers[i - 1].OpOpMatchWinPercent) < 0.001 &&
                        activePlayers[i].HeadToHeadPosition == activePlayers[i - 1].HeadToHeadPosition)
                    {
                        // Same position as previous player since all tiebreakers are equal
                        activePlayers[i].Position = activePlayers[i - 1].Position;
                    }
                    else
                    {
                        // New position starting from where we are in the list
                        currentPosition = i + 1;
                        activePlayers[i].Position = currentPosition;
                    }
                }

                // Disqualified players get no position (stays 0)
                foreach (var disqualifiedPlayer in allPlayers.Where(p => p.IsDisqualified))
                {
                    disqualifiedPlayer.Position = 0;
                }
            }

            return standings;
