using System;
using FXDemo.Contracts;

namespace FXDemo.Models
{
    public class GeneralStatisiticResponce : IGeneralReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
    }
}
