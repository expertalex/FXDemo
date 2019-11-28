using System;
using FXDemo.Contracts;

namespace FXDemo.Models.Http
{
    public class ManagerRequest : IManager
    {
        public string Name { get ; set ; }
        public string TeamName { get; set; }
        public int RedCards { get ; set; }
        public int YellowCards { get ; set ; }
    }
}
