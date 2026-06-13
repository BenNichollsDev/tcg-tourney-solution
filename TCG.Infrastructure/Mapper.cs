using System;
using System.Collections.Generic;
using System.Text;
using TCG.Application.Dtos;
using TCG.Domain.Entities;
using AutoMapper;
//REF AUTOMAPPER 01/02/2026

namespace TCG.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Entity to Dto mappings
            CreateMap<League, LeagueDto>()
                .ForMember(d => d.LeagueId, o => o.MapFrom(s => s.LeagueId))
                .ForMember(d => d.LeagueName, o => o.MapFrom(s => s.LeagueName))
                .ForMember(d => d.LeagueGame, o => o.MapFrom(s => s.LeagueGame))
                .ForMember(d => d.LeagueDescription, o => o.MapFrom(s => s.LeagueDescription));

            CreateMap<Tournament, TournamentDto>()
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.TournamentId))
                .ForMember(d => d.TournamentLeague, o => o.MapFrom(s => s.LeagueId))
                .ForMember(d => d.TournamentName, o => o.MapFrom(s => s.TournamentName))
                .ForMember(d => d.TournamentGame, o => o.MapFrom(s => s.TournamentGame))
                .ForMember(d => d.TournamentFormat, o => o.MapFrom(s => s.TournamentFormat))
                .ForMember(d => d.TournamentRequireDeck, o => o.MapFrom(s => s.TournamentRequireDeck))
                .ForMember(d => d.TournamentRoundNum, o => o.MapFrom(s => s.TournamentRoundNum))
                .ForMember(d => d.TournamentMaxRoundNum, o => o.MapFrom(s => s.TournamentMaxRoundNum))
                .ForMember(d => d.TournamentDescription, o => o.MapFrom(s => s.TournamentDescription))
                .ForMember(d => d.TournamentPairing, o => o.MapFrom(s => s.TournamentPairing))
                .ForMember(d => d.TournamentDate, o => o.MapFrom(s => s.TournamentDate))
                .ForMember(d => d.TournamentTime, o => o.MapFrom(s => s.TournamentTime))
                .ForMember(d => d.TournamentEntryFee, o => o.MapFrom(s => s.TournamentEntryFee))
                .ForMember(d => d.TournamentMaxParticipants, o => o.MapFrom(s => s.TournamentMaxParticipants))
                .ForMember(d => d.TournamentSwissTopcut, o => o.MapFrom(s => s.TournamentSwissTopcut))
                .ForMember(d => d.TournamentSwissTopcutNum, o => o.MapFrom(s => s.TournamentSwissTopcutNum))
                .ForMember(d => d.TournamentStarted, o => o.MapFrom(s => s.TournamentStarted))
                .ForMember(d => d.TournamentRoundInProgress, o => o.MapFrom(s => s.TournamentRoundInProgress))
                .ForMember(d => d.TournamentFinished, o => o.MapFrom(s => s.TournamentFinished))
                .ForMember(d => d.TournamentCancelled, o => o.MapFrom(s => s.TournamentCancelled));

            CreateMap<TournamentPlayer, TournamentPlayerDto>()
                .ForMember(d => d.TournamentPlayerId, o => o.MapFrom(s => s.TournamentPlayerId))
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.TournamentId))
                .ForMember(d => d.PlayerId, o => o.MapFrom(s => 0))
                .ForMember(d => d.PlayerName, o => o.MapFrom(s => s.PlayerName))
                .ForMember(d => d.PlayerRoundRobinWins, o => o.MapFrom(s => s.PlayerRoundRobinWins))
                .ForMember(d => d.PlayerRoundRobinDraws, o => o.MapFrom(s => s.PlayerRoundRobinDraws))
                .ForMember(d => d.PlayerRoundRobinLosses, o => o.MapFrom(s => s.PlayerRoundRobinLosses))
                .ForMember(d => d.PlayerRoundRobinScore, o => o.MapFrom(s => s.PlayerRoundRobinScore))
                .ForMember(d => d.PlayerRoundRobinMatchPoints, o => o.MapFrom(s => s.PlayerRoundRobinMatchPoints))
                .ForMember(d => d.PlayerRoundRobinPoints, o => o.MapFrom(s => s.PlayerRoundRobinPoints))
                .ForMember(d => d.PlayerSwissWins, o => o.MapFrom(s => s.PlayerSwissWins))
                .ForMember(d => d.PlayerSwissDraws, o => o.MapFrom(s => s.PlayerSwissDraws))
                .ForMember(d => d.PlayerSwissLosses, o => o.MapFrom(s => s.PlayerSwissLosses))
                .ForMember(d => d.PlayerSwissScore, o => o.MapFrom(s => s.PlayerSwissScore))
                .ForMember(d => d.PlayerSwissMatchPoints, o => o.MapFrom(s => s.PlayerSwissMatchPoints))
                .ForMember(d => d.PlayerSwissPoints, o => o.MapFrom(s => s.PlayerSwissPoints))
                .ForMember(d => d.PlayerBye, o => o.MapFrom(s => s.PlayerBye))
                .ForMember(d => d.GamesPlayed, o => o.MapFrom(s => s.GamesPlayed))
                .ForMember(d => d.TpDisqualified, o => o.MapFrom(s => s.TpDisqualified))
                .ForMember(d => d.TpBye, o => o.MapFrom(s => s.TpBye))
                .ForMember(d => d.TpDropped, o => o.MapFrom(s => s.TpDropped))
                .ForMember(d => d.TpPosition, o => o.MapFrom(s => s.TpPosition));

            CreateMap<Staff, StaffDto>()
                .ForMember(d => d.StaffId, o => o.MapFrom(s => s.StaffId))
                .ForMember(d => d.StaffFirstName, o => o.MapFrom(s => s.StaffFirstName))
                .ForMember(d => d.StaffSurname, o => o.MapFrom(s => s.StaffSurname))
                .ForMember(d => d.StaffEmail, o => o.MapFrom(s => s.StaffEmail))
                .ForMember(d => d.StaffPassword, o => o.MapFrom(s => s.StaffPassword))
                .ForMember(d => d.StaffMobile, o => o.MapFrom(s => s.StaffMobile))
                .ForMember(d => d.StaffRoleManagement, o => o.MapFrom(s => s.StaffRoleManagement))
                .ForMember(d => d.StaffRoleHead, o => o.MapFrom(s => s.StaffRoleHead));

            // Dto to Entity mappings
            CreateMap<LeagueDto, League>()
                .ForMember(d => d.LeagueId, o => o.MapFrom(s => s.LeagueId))
                .ForMember(d => d.LeagueName, o => o.MapFrom(s => s.LeagueName))
                .ForMember(d => d.LeagueGame, o => o.MapFrom(s => s.LeagueGame))
                .ForMember(d => d.LeagueDescription, o => o.MapFrom(s => s.LeagueDescription));

            CreateMap<TournamentDto, Tournament>()
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.TournamentId))
                .ForMember(d => d.LeagueId, o => o.MapFrom(s => s.TournamentLeague))
                .ForMember(d => d.TournamentName, o => o.MapFrom(s => s.TournamentName))
                .ForMember(d => d.TournamentGame, o => o.MapFrom(s => s.TournamentGame))
                .ForMember(d => d.TournamentFormat, o => o.MapFrom(s => s.TournamentFormat))
                .ForMember(d => d.TournamentRequireDeck, o => o.MapFrom(s => s.TournamentRequireDeck))
                .ForMember(d => d.TournamentRoundNum, o => o.MapFrom(s => s.TournamentRoundNum))
                .ForMember(d => d.TournamentMaxRoundNum, o => o.MapFrom(s => s.TournamentMaxRoundNum))
                .ForMember(d => d.TournamentDescription, o => o.MapFrom(s => s.TournamentDescription))
                .ForMember(d => d.TournamentPairing, o => o.MapFrom(s => s.TournamentPairing))
                .ForMember(d => d.TournamentDate, o => o.MapFrom(s => s.TournamentDate))
                .ForMember(d => d.TournamentTime, o => o.MapFrom(s => s.TournamentTime))
                .ForMember(d => d.TournamentEntryFee, o => o.MapFrom(s => s.TournamentEntryFee))
                .ForMember(d => d.TournamentMaxParticipants, o => o.MapFrom(s => s.TournamentMaxParticipants))
                .ForMember(d => d.TournamentSwissTopcut, o => o.MapFrom(s => s.TournamentSwissTopcut))
                .ForMember(d => d.TournamentSwissTopcutNum, o => o.MapFrom(s => s.TournamentSwissTopcutNum))
                .ForMember(d => d.TournamentStarted, o => o.MapFrom(s => s.TournamentStarted))
                .ForMember(d => d.TournamentRoundInProgress, o => o.MapFrom(s => s.TournamentRoundInProgress))
                .ForMember(d => d.TournamentFinished, o => o.MapFrom(s => s.TournamentFinished))
                .ForMember(d => d.TournamentCancelled, o => o.MapFrom(s => s.TournamentCancelled));

            CreateMap<TournamentPlayerDto, TournamentPlayer>()
                .ForMember(d => d.TournamentPlayerId, o => o.MapFrom(s => s.TournamentPlayerId))
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.TournamentId))
                .ForMember(d => d.PlayerName, o => o.MapFrom(s => s.PlayerName))
                .ForMember(d => d.PlayerRoundRobinWins, o => o.MapFrom(s => s.PlayerRoundRobinWins))
                .ForMember(d => d.PlayerRoundRobinDraws, o => o.MapFrom(s => s.PlayerRoundRobinDraws))
                .ForMember(d => d.PlayerRoundRobinLosses, o => o.MapFrom(s => s.PlayerRoundRobinLosses))
                .ForMember(d => d.PlayerRoundRobinScore, o => o.MapFrom(s => s.PlayerRoundRobinScore))
                .ForMember(d => d.PlayerRoundRobinMatchPoints, o => o.MapFrom(s => s.PlayerRoundRobinMatchPoints))
                .ForMember(d => d.PlayerRoundRobinPoints, o => o.MapFrom(s => s.PlayerRoundRobinPoints))
                .ForMember(d => d.PlayerSwissWins, o => o.MapFrom(s => s.PlayerSwissWins))
                .ForMember(d => d.PlayerSwissDraws, o => o.MapFrom(s => s.PlayerSwissDraws))
                .ForMember(d => d.PlayerSwissLosses, o => o.MapFrom(s => s.PlayerSwissLosses))
                .ForMember(d => d.PlayerSwissScore, o => o.MapFrom(s => s.PlayerSwissScore))
                .ForMember(d => d.PlayerSwissMatchPoints, o => o.MapFrom(s => s.PlayerSwissMatchPoints))
                .ForMember(d => d.PlayerSwissPoints, o => o.MapFrom(s => s.PlayerSwissPoints))
                .ForMember(d => d.PlayerBye, o => o.MapFrom(s => s.PlayerBye))
                .ForMember(d => d.GamesPlayed, o => o.MapFrom(s => s.GamesPlayed))
                .ForMember(d => d.TpDisqualified, o => o.MapFrom(s => s.TpDisqualified))
                .ForMember(d => d.TpBye, o => o.MapFrom(s => s.TpBye))
                .ForMember(d => d.TpDropped, o => o.MapFrom(s => s.TpDropped))
                .ForMember(d => d.TpPosition, o => o.MapFrom(s => s.TpPosition));

            CreateMap<Pairing, PairingDto>()
                .ForMember(d => d.PairingId, o => o.MapFrom(s => s.PairingId))
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.TournamentId))
                .ForMember(d => d.RoundNumber, o => o.MapFrom(s => s.RoundNumber))
                .ForMember(d => d.Player1Id, o => o.MapFrom(s => s.Player1Id))
                .ForMember(d => d.Player2Id, o => o.MapFrom(s => s.Player2Id))
                .ForMember(d => d.Player1Score, o => o.MapFrom(s => s.Player1Score))
                .ForMember(d => d.Player2Score, o => o.MapFrom(s => s.Player2Score))
                .ForMember(d => d.WinnerId, o => o.MapFrom(s => s.WinnerId))
                .ForMember(d => d.Player1GameCount, o => o.MapFrom(s => s.Player1GameCount))
                .ForMember(d => d.Player2GameCount, o => o.MapFrom(s => s.Player2GameCount))
                .ForMember(d => d.HasResult, o => o.MapFrom(s => s.HasResult))
                .ForMember(d => d.PairingDraw, o => o.MapFrom(s => s.PairingDraw));

            CreateMap<PairingDto, Pairing>()
                .ForMember(d => d.PairingId, o => o.MapFrom(s => s.PairingId))
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.TournamentId))
                .ForMember(d => d.RoundNumber, o => o.MapFrom(s => s.RoundNumber))
                .ForMember(d => d.Player1Id, o => o.MapFrom(s => s.Player1Id))
                .ForMember(d => d.Player2Id, o => o.MapFrom(s => s.Player2Id))
                .ForMember(d => d.Player1Score, o => o.MapFrom(s => s.Player1Score))
                .ForMember(d => d.Player2Score, o => o.MapFrom(s => s.Player2Score))
                .ForMember(d => d.WinnerId, o => o.MapFrom(s => s.WinnerId))
                .ForMember(d => d.Player1GameCount, o => o.MapFrom(s => s.Player1GameCount))
                .ForMember(d => d.Player2GameCount, o => o.MapFrom(s => s.Player2GameCount))
                .ForMember(d => d.HasResult, o => o.MapFrom(s => s.HasResult))
                .ForMember(d => d.PairingDraw, o => o.MapFrom(s => s.PairingDraw));

            CreateMap<StaffDto, Staff>()
                .ForMember(d => d.StaffId, o => o.MapFrom(s => s.StaffId))
                .ForMember(d => d.StaffFirstName, o => o.MapFrom(s => s.StaffFirstName))
                .ForMember(d => d.StaffSurname, o => o.MapFrom(s => s.StaffSurname))
                .ForMember(d => d.StaffEmail, o => o.MapFrom(s => s.StaffEmail))
                .ForMember(d => d.StaffPassword, o => o.MapFrom(s => s.StaffPassword))
                .ForMember(d => d.StaffMobile, o => o.MapFrom(s => s.StaffMobile))
                .ForMember(d => d.StaffRoleManagement, o => o.MapFrom(s => s.StaffRoleManagement))
                .ForMember(d => d.StaffRoleHead, o => o.MapFrom(s => s.StaffRoleHead));

            CreateMap<Player, PlayerDto>()
                .ForMember(d => d.PlayerId, o => o.MapFrom(s => s.PlayerId))
                .ForMember(d => d.PlayerFirstName, o => o.MapFrom(s => s.PlayerFirstName))
                .ForMember(d => d.PlayerLastName, o => o.MapFrom(s => s.PlayerLastName))
                .ForMember(d => d.PlayerEmail, o => o.MapFrom(s => s.PlayerEmail))
                .ForMember(d => d.PlayerPhone, o => o.MapFrom(s => s.PlayerPhone))
                .ForMember(d => d.PlayerDOB, o => o.MapFrom(s => s.PlayerDOB))
                .ForMember(d => d.PlayerGender, o => o.MapFrom(s => s.PlayerGender));

            CreateMap<PlayerDto, Player>()
                .ForMember(d => d.PlayerId, o => o.MapFrom(s => s.PlayerId))
                .ForMember(d => d.PlayerFirstName, o => o.MapFrom(s => s.PlayerFirstName))
                .ForMember(d => d.PlayerLastName, o => o.MapFrom(s => s.PlayerLastName))
                .ForMember(d => d.PlayerEmail, o => o.MapFrom(s => s.PlayerEmail))
                .ForMember(d => d.PlayerPhone, o => o.MapFrom(s => s.PlayerPhone))
                .ForMember(d => d.PlayerDOB, o => o.MapFrom(s => s.PlayerDOB))
                .ForMember(d => d.PlayerGender, o => o.MapFrom(s => s.PlayerGender));
        }
    }
}
