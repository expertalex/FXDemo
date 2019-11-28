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

            CreateMap<PlayerRequest, Player>();
            CreateMap<Player, PlayerResponse>();

            CreateMap<RefereeRequest, Referee>();
            CreateMap<Referee, RefereeResponse>();

            CreateMap<ManagerRequest, Manager>();
            CreateMap<Manager, ManagerResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => -src.Id)); // Very Important in order to return -n as id for Managers

            CreateMap<MatchRequest, Match>();
            CreateMap<Match, MatchResponse>();


            CreateMap<Player, MinuteResponse>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.MinutesPlayed));
            CreateMap<Referee, MinuteResponse>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.MinutesPlayed));

            // TODO: Think about better desing...
            CreateMap<Manager, YelowCardResponse>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.YellowCards));
            CreateMap<Player, YelowCardResponse>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.YellowCards));

            CreateMap<Manager, RedCardResponse>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.YellowCards));
            CreateMap<Player, RedCardResponse>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.YellowCards));


            // TODO: Remove 2
            CreateMap<PlayerRequest, PlayerResponse>();
            CreateMap<Player, PlayerRequest>();


        }
    }
}
