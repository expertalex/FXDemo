using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FXDemo.Models
{
    public class Manager
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Team")]
        public string TeamName { get; set; }
        public Team Team { get; set; }

        [NotMapped]
        public int ControllerId
        {
            get
            {
                return -Id;
            }
        }

        [NotMapped]
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
        

        public int YellowCards { get; set; }

        public int RedCards { get; set; }

        public int MinutesPlayed { get; set; }


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
