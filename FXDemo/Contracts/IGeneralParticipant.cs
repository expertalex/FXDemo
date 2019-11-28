using System;
using FXDemo.Models;

namespace FXDemo.Contracts
{

    public interface IGeneralParticipant
    {
        int Id { get; set; }
        public string Name { get; set; }
        public int MinutesPlayed { get; set; }
    }

    public interface ITeamMember
    {
        public string TeamName { get; set; }
        public Team Team { get; set; }
    }

    public interface ICardOwner
    {
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
    }


    public interface IPlayer : IGeneralParticipant, ICardOwner
    {
        public int Number { get; set; }
        public string TeamName { get; set; }

    }




}
