using System;
using System.Text.Json.Serialization;
using FXDemo.Contracts;

namespace FXDemo.Models.Http
{
    public class PlayerRequest : IPlayer
    {

        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Number { get; set; }
        public string TeamName { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int MinutesPlayed { get; set; }

        // Not needes since Automaper...
        /*
        public PlayerRequest(Player player)
        {
            this.Id = player.Id;
            this.Name = player.Name;
            this.Number = player.Number;
            this.TeamName = player.TeamName;

            this.YellowCards = player.YellowCards;
            this.RedCards = player.RedCards;
            this.MinutesPlayed = player.MinutesPlayed;
        }
        */

    }
}
