using LandonApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXDemo.Models.Http
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
