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

    public interface IParticipantName
    {
        public string Name { get; set; }
    }

    public interface ITeamMember
    {
        public string TeamName { get; set; }
        public Team Team { get; set; }

        public bool IsPlayer();
        public bool IsManager();
    }


    public interface ICardOwner
    {
        public int RedCards { get; set; }
        public int YellowCards { get; set; }
    }


    // TODO: Remove IParticipantId
    public interface IPlayer : IParticipantId, IParticipantName, ICardOwner, IPlayeTime
    {
        public int Number { get; set; }
        public string TeamName { get; set; }

    }

    public interface IReferee : IParticipantName, IPlayeTime
    {
    }

    public interface IManager : IParticipantName, ICardOwner
    {
        public string TeamName { get; set; }

    }


    public interface IMatch 
    {
        // TODO
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }

    public interface IGeneralReport
    {
        int Id { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
    }




}
