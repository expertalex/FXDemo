﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FXDemo.Contracts;

namespace FXDemo.Models
{


    // Acts like player responce, becuase it is the most complete.
    // TODO: Automaping
    public class Player : IPlayer, ITeamMember
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [ForeignKey("Team")]
        public string TeamName { get; set; }
        [BindNever]
        [JsonIgnore]
        public Team Team { get; set; }

        [NotMapped]
        [BindNever]
        [JsonIgnore]
        public int ControllerId
        {
            get
            {
                return Id;
            }
        }

        [NotMapped]
        [BindNever]
        [JsonIgnore]
        public string TeamId
        {
            get
            {
                return this.TeamName;
            }
            set
            {
                this.TeamName = value;
            }
        }

        [Required]
        public int YellowCards { get; set; }
        [Required]
        public int RedCards { get; set; }
        [Required]
        public int MinutesPlayed { get; set; }

        [JsonIgnore]
        [BindNever]
        [InverseProperty("Player")]
        public ICollection<MatchPlayersHouse> HouseTeamPlayers { get; set; }

        [JsonIgnore]
        [BindNever]
        [InverseProperty("Player")]
        public ICollection<MatchPlayersAway> AwayTeamPlayers { get; set; }

    }
}