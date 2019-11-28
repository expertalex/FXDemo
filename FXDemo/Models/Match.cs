using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace FXDemo.Models
{
    public class Match
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Important: One match always will have only 2 Teams:
        // 2 Managers and 22 Players (11 per Team) in total
        public ICollection<MatchPlayersHouse> HouseTeamPlayers { get; set; }

        public ICollection<MatchPlayersAway> AwayTeamPlayers { get; set; }


        // It indicates that a match has one, and only one, HouseTeamManager.
        [ForeignKey("HouseTeamManager")]
        public int HouseTeamManagerId { get; set; }
        public Manager HouseTeamManager { get; set; }


        // It indicates that a match has one, and only one, AwayTeamManager.
        [ForeignKey("AwayTeamManager")]
        public int AwayTeamManagerId { get; set; }
        public Manager AwayTeamManager { get; set; }


        // It indicates that a match has one, and only one, Referee.
        [ForeignKey("Referee")]
        public int RefereeId { get; set; }
        public Referee Referee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
