using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FXDemo.Contracts;

namespace FXDemo.Models
{
    public class Manager: IParticipantId, IManager, ITeamMember
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

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


        [NotMapped]
        [JsonIgnore]
        public int ControllerId
        {
            get
            {
                return -Id; // Important:  Convetion (-n = Managers, +n Players)
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


        /* Not implemented to avoid manual maping....
        [ForeignKey("HouseTeamManagerId")]
        public ICollection<Match> HomeMatches { get; set; }

        [ForeignKey("AwayTeamManagerId")]
        public ICollection<Match> AwayMatches { get; set; }

        [NotMapped]
        public ICollection<Match> Matches {
            get
            {
                var newList = new List<Match>(AwayMatches);
                newList.AddRange(HomeMatches);

                return newList;
            }
        }
        */
    }
}
