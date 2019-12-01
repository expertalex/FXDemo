using System;
using FXDemo.Models;

namespace FXDemo.Contracts
{

    interface IParticipantId
    {
        int Id { get; set; }
    }

    interface IPlayeTime
    {
        int MinutesPlayed { get; set; }
    }

    interface IParticipantName
    {
        string Name { get; set; }
    }

    interface ITeamMember
    {
        string TeamName { get; set; }
        Team Team { get; set; }

        public int ControllerId { get; }

    }


    interface ICardOwner
    {
        int RedCards { get; set; }
        int YellowCards { get; set; }
    }

    interface IPlayer : IParticipantName, ICardOwner, IPlayeTime
    {
        int Number { get; set; }
        string TeamName { get; set; }

    }

    interface IReferee : IParticipantName, IPlayeTime
    {
    }

    interface IManager : IParticipantName, ICardOwner
    {
        string TeamName { get; set; }

    }


    interface IMatch 
    {
        string Name { get; set; }
        DateTime Date { get; set; }
    }

    interface IGeneralReport
    {
        int Id { get; set; }
        string Name { get; set; }
        int Total { get; set; }
    }




}
