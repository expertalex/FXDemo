using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FXDemo.Contracts;

namespace FXDemo.Models.Http
{
    public class MatchRequest : IMatch
    {
        [Required]
        public string Name { get; set; }

        // Important: One match always will have only 2 Teams:
        // 2 Managers and 22 Players (11 per Team) in total
        public ICollection<int> HouseTeam { get; set; }
        public ICollection<int> AwayTeam { get; set; }

        [Required]
        public int Referee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
