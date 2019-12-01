﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using FXDemo.Contracts;
using FXDemo.Models.Http;

namespace FXDemo.Models.Http
{
    public class MatchResponse : IMatch
    {
       
        [Required]
        public string Name { get; set; }

        // Important: One match always will have only 2 Teams:
        // 2 Managers and 22 Players in total (11 players + 1 Manager per Team)
        // TODO: Return and Map PlayerResponse
        public ICollection<Player> HouseTeamPlayers { get; set; }
        public ICollection<Player> AwayTeamPlayers { get; set; }

        public ManagerResponse HouseTeamManager { get; set; }
        public ManagerResponse AwayTeamManager { get; set; }

        public RefereeResponse Referee { get; set; }

        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd'T'HH:mm:ssZ}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd'T'HH:mm:ssZ}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        /*
        public MatchResponse(Match match)
        {
            Name = match.Name;
            HouseTeamManager = match.HouseTeamManager;
            AwayTeamManager = match.AwayTeamManager;
            Referee = match.Referee;
            Date = match.Date;
            HouseTeamPlayers = match.HouseTeamPlayers.Select(o => o.Player).ToList();
            AwayTeamPlayers = match.AwayTeamPlayers.Select(o => o.Player).ToList();
        }
        */

    }
}
