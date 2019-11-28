﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FXDemo.Contracts;

namespace FXDemo.Models
{
    public class Manager: IManager
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

        [Required]
        [DefaultValue(0)]
        public int YellowCards { get; set; }

        [Required]
        [DefaultValue(0)]
        public int RedCards { get; set; }



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
