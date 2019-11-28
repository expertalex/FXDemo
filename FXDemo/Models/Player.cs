using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FXDemo.Contracts;
using System.ComponentModel;

namespace FXDemo.Models
{

    // Acts like player responce, becuase it is the most complete.
    // TODO: Automaping
    public class Player : IParticipantId, IPlayer, ITeamMember
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
        
        [JsonIgnore]
        public Team Team { get; set; }

        [NotMapped]
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
        [DefaultValue(0)]
        public int YellowCards { get; set; }
        [Required]
        [DefaultValue(0)]
        public int RedCards { get; set; }
        [Required]
        [DefaultValue(0)]
        public int MinutesPlayed { get; set; }

        [JsonIgnore]
        [InverseProperty("Player")]
        public ICollection<MatchPlayersHouse> HouseTeamPlayers { get; set; }

        [JsonIgnore]
        [InverseProperty("Player")]
        public ICollection<MatchPlayersAway> AwayTeamPlayers { get; set; }

        [NotMapped]
        [JsonIgnore]
        public int ControllerId
        {
            get
            {
                return Id;
            }
        }

        public bool IsManager()
        {
            return this.ControllerId < 0;
        }

        public bool IsPlayer()
        {
            return this.ControllerId > 0;
        }


    }
}
