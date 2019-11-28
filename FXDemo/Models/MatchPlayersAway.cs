using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FXDemo.Models
{
    public class MatchPlayersAway
    {
        public int Id { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public Match Match { get; set; }
    }
}
