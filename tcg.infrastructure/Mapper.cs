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
            CreateMap<League, LeagueDto>();
            CreateMap<Tournament, TournamenTDto>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Pairing, PairingDto>();
            CreateMap<Match, MatchDto>();
            CreateMap<Staff, StaffDto>();

            // Dto to Entity mappings
            CreateMap<LeagueDto, League>();
            CreateMap<TournamenTDto, Tournament>();
            CreateMap<PlayerDto, Player>();
            CreateMap<PairingDto, Pairing>();
            CreateMap<MatchDto, Match>();
            CreateMap<StaffDto, Staff>();
        }
    }
}
