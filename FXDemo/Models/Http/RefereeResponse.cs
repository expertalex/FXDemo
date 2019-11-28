using System;
using FXDemo.Contracts;

namespace FXDemo.Models.Http
{
    public class RefereeResponse : IParticipantId, IReferee
    {
        public int Id { get; set; }
        public string Name { get; set ; }
        public int MinutesPlayed { get; set; }
    }
}
