using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FXDemo.Models
{
    public class Referee
    {
        /*
        public Referee()
        {
            this.Matches = new HashSet<Match>();
        }
        */

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int MinutesPlayed { get; set; }

        // This last one will be used by Entity Framework Core, the ORM most ASP.NET Core applications use to persist data into a database,
        // to map the relationship between Referee and Matche. It also makes sense thinking in terms of object-oriented programming, since a Referee
        // has many related Matches.
        public ICollection<Match> Matches { get; set; }
    }
}
