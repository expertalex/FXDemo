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

            // CreateMap<Referee, RefereeRequest>();
            CreateMap<RefereeRequest, Referee>();
            CreateMap<RefereeRequest, Manager>();
            CreateMap<RefereeRequest, Match>();
            CreateMap<RefereeRequest, Match>();

            // TODO: Map both and use both.... 
            /*
            CreateMap<Manager, ManagerResponse>();
            CreateMap<Manager, ManagerRequest>();

            CreateMap<Referee, RefereeResponse>();
            CreateMap<Referee, RefereeRequest>();

            CreateMap<Match, MatchResponse>();
            CreateMap<Match, MatchRequest>();

            CreateMap<Card, CardResponse>();
            CreateMap<Minute, MinuteResponse>();
            */


        }
    }
}
