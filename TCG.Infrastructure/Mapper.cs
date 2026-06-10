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
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<Tournament, TournamentDto>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<TournamentPlayer, TournamentPlayerDto>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<Pairing, PairingDto>()
                .ForMember(d => d.PairingTournamentId, o => o.MapFrom(s => s.TournamentId))
                .ForMember(d => d.PairingTp1, o => o.MapFrom(s => s.Player1Id))
                .ForMember(d => d.PairingTp2, o => o.MapFrom(s => s.Player2Id))
                .ForMember(d => d.PairingPlayer1Score, o => o.MapFrom(s => s.Player1Score))
                .ForMember(d => d.PairingPlayer2Score, o => o.MapFrom(s => s.Player2Score))
                .ForMember(d => d.PairingPlayer1GameCount, o => o.MapFrom(s => s.Player1GameCount))
                .ForMember(d => d.PairingPlayer2GameCount, o => o.MapFrom(s => s.Player2GameCount))
                .ForMember(d => d.PairingHasResult, o => o.MapFrom(s => s.HasResult))
                .ForMember(d => d.RoundNumber, o => o.MapFrom(s => s.RoundNumber));

            CreateMap<Staff, StaffDto>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));


            // Dto to Entity mappings
            CreateMap<LeagueDto, League>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<TournamentDto, Tournament>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<TournamentPlayerDto, TournamentPlayer>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<PairingDto, Pairing>()
                .ForMember(d => d.TournamentId, o => o.MapFrom(s => s.PairingTournamentId))
                .ForMember(d => d.Player1Id, o => o.MapFrom(s => s.PairingTp1))
                .ForMember(d => d.Player2Id, o => o.MapFrom(s => s.PairingTp2))
                .ForMember(d => d.Player1Score, o => o.MapFrom(s => s.PairingPlayer1Score))
                .ForMember(d => d.Player2Score, o => o.MapFrom(s => s.PairingPlayer2Score))
                .ForMember(d => d.Player1GameCount, o => o.MapFrom(s => s.PairingPlayer1GameCount))
                .ForMember(d => d.Player2GameCount, o => o.MapFrom(s => s.PairingPlayer2GameCount))
                .ForMember(d => d.HasResult, o => o.MapFrom(s => s.PairingHasResult))
                .ForMember(d => d.RoundNumber, o => o.MapFrom(s => s.RoundNumber));

            CreateMap<StaffDto, Staff>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));
        }
    }
}
