using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
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
        [JsonIgnore]
        public string Name {
            get{
                return this.TeamName;
            }
            set
            {
                this.TeamName = value;
            }
        }

        [JsonIgnore]
        public ICollection<Player> Players { get; set; }

        [JsonIgnore]
        public ICollection<Manager> Managers { get; set; }


}
}
