using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FXDemo.Contracts;

namespace FXDemo.Models.Http
{
    public class RefereeRequest :IReferee
    {
        /*
        [NotMapped]
        [JsonIgnore]
        public int Id { get; set; }
        */

        public string Name { get; set; }
        public int MinutesPlayed { get; set; }
    }
}
