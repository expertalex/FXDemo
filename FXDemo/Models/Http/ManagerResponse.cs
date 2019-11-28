using System;
using FXDemo.Contracts;

namespace FXDemo.Models.Http
{
    public class ManagerResponse : IParticipantId, IManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public int RedCards { get; set; }
        public int YellowCards { get; set; }
    }
}
