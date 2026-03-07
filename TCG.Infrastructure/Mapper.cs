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

            CreateMap<Pairing, PairingDto>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<Match, MatchDto>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

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

            CreateMap<PairingDto, Pairing>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<MatchDto, Match>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));

            CreateMap<StaffDto, Staff>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));




        }
    }
}
