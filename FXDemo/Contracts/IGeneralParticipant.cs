using System;
using FXDemo.Models;

namespace FXDemo.Contracts
{

    public interface IParticipantId
    {
        int Id { get; set; }
    }

    public interface IPlayeTime
    {
        public int MinutesPlayed { get; set; }
    }

    public interface IGeneralParticipant 
    {
        public string Name { get; set; }
    }

    public interface ITeamMember
    {
        public string TeamName { get; set; }
        public Team Team { get; set; }
    }


    public interface ICardOwner
    {
        public int RedCards { get; set; }
        public int YellowCards { get; set; }
    }


    public interface IPlayer : IParticipantId, IGeneralParticipant, ICardOwner, IPlayeTime
    {
        public int Number { get; set; }
        public string TeamName { get; set; }

    }

    public interface IReferee : IGeneralParticipant, IPlayeTime
    {
    }

    public interface IManager : IGeneralParticipant, ICardOwner
    {
        public string TeamName { get; set; }

    }




}
