using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FXDemo.Models
{
    public class MatchPlayersAway
    {
        // TODO: Remove id and have composit PlayerId & MatchId
        [Key]
        public int Id { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }
        [JsonIgnore]
        public Match Match { get; set; }
    }
}
