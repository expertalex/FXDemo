using System;
using AutoMapper;
using FXDemo.Models;
using FXDemo.Models.Http;

namespace FXDemo.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Player, PlayerResponse>();
            CreateMap<Player, PlayerRequest>();
            CreateMap<PlayerRequest, Player>();
            CreateMap<PlayerRequest, PlayerResponse>();

            /*
            CreateMap<Manager, ManagerResponse>();
            CreateMap<Manager, ManagerRequest>();

            CreateMap<Referee, RefereeResponse>();
            CreateMap<Manager, RefereeRequest>();

            CreateMap<Match, MatchResponse>();
            CreateMap<Match, MatchRequest>();

            CreateMap<Card, CardResponse>();
            CreateMap<Minute, MinuteResponse>();
            */


        }
    }
}
