using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FXDemo.Models
{
    public class Team
    {

        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Managers = new HashSet<Manager>();
        }

       

        [Key]
        [Required]
        public string TeamName { get; set; }

        [NotMapped]
        public string Id
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

        [NotMapped]
        public string Name {
            get{
                return this.TeamName;
            }
            set
            {
                this.TeamName = value;
            }
        }


        public ICollection<Player> Players { get; set; }

        public ICollection<Manager> Managers { get; set; }


}
}
