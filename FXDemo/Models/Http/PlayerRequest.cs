using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FXDemo.Contracts;

namespace FXDemo.Models.Http
{
    public class PlayerRequest : IPlayer
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string TeamName { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int MinutesPlayed { get; set; }

    }
}
