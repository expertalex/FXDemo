using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using FXDemo.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FXDemo.Models
{
    public class Match : IParticipantId , IMatch
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Important: One match always will have only 2 Teams:
        // 2 Managers and 22 Players (11 per Team) in total (in this design for simplicity one player can be in both matches)
        // If in each match 22 players always shood be unique, we can refactor the desing and have only one TeamPlayers table and filter by team
        public ICollection<MatchPlayersHouse> HouseTeamPlayers { get; set; } = new List<MatchPlayersHouse>();
        public ICollection<MatchPlayersAway> AwayTeamPlayers { get; set; } = new List<MatchPlayersAway>();


        // It indicates that a match has one, and only one, HouseTeamManager.
        [JsonIgnore]
        [ForeignKey("HouseTeamManager")]
        public int HouseTeamManagerId { get; set; }
        public Manager HouseTeamManager { get; set; }


        // It indicates that a match has one, and only one, AwayTeamManager.
        [JsonIgnore]
        [ForeignKey("AwayTeamManager")]
        public int AwayTeamManagerId { get; set; }
        public Manager AwayTeamManager { get; set; }


        // It indicates that a match has one, and only one, Referee.
        [JsonIgnore]
        [ForeignKey("Referee")]
        public int RefereeId { get; set; }
        public Referee Referee { get; set; }


        // Format: {0:s}
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd'T'HH:mm:ssZ}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:s}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


    }
}
