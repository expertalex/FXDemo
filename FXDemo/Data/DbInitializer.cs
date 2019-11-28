using System;
using System.Linq;
using FXDemo.Models;

namespace FXDemo.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FXDataContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any Teams.
            if (context.Team.Any())
            {
                return;   // DB has been seeded
            }

            // Look for any Players.
            if (context.Player.Any())
            {
                return;   // DB has been seeded
            }

            // Look for any Players.
            if (context.Manager.Any())
            {
                return;   // DB has been seeded
            }

            if (context.Referee.Any())
            {
                return;   // DB has been seeded
            }

            var teams = new Team[]
            {
                new Team { TeamName = "FCB" },
                new Team { TeamName = "Madrid" },
                new Team { TeamName = "Atletic" },
                new Team { TeamName = "Manchester" },
                new Team { TeamName = "Juventus" }

            };
            foreach (Team team in teams)
            {
                context.Team.Add(team);
            }
            context.SaveChanges();


            var players = new Player[]
            {
                new Player{ Name="Messi",   Number=10,  TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Player{ Name="Pique",   Number=3,   TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Player{ Name="Suarez",  Number=5,   TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Player{ Name="Vidal",   Number=6,   TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Player{ Name="Dembelé", Number=8,   TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Player{ Name="Carles",  Number=11,  TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },

                new Player{ Name="Roni", Number=10, TeamName=teams[1].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Player{ Name="Sergio", Number=4, TeamName=teams[1].TeamName, YellowCards=3, RedCards=4, MinutesPlayed=5 }

            };
            foreach (Player  player in players)
            {
                context.Player.Add(player);
            }
            context.SaveChanges();


            var managers = new Manager[]
            {
                new Manager{ Name="Pep", TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Manager{ Name="Pep Helper", TeamName=teams[0].TeamName, YellowCards=0, RedCards=0, MinutesPlayed=0 },
                new Manager{ Name="Zidan", TeamName=teams[1].TeamName, YellowCards=1, RedCards=2, MinutesPlayed=3 },
                new Manager{ Name="Zidan Helper", TeamName=teams[1].TeamName, YellowCards=10, RedCards=11, MinutesPlayed=12 },

            };
            foreach (Manager manager in managers)
            {
                context.Manager.Add(manager);
            }
            context.SaveChanges();


            var referees = new Referee[]
            {
                new Referee { Name = "Alex", MinutesPlayed = 0 },
                new Referee { Name = "Marck", MinutesPlayed = 0 },
                new Referee { Name = "Joan", MinutesPlayed = 0 },

            };
            foreach (Referee referee in referees)
            {
                context.Referee.Add(referee);
            }
            context.SaveChanges();



            var matches = new Match[]
            {
                new Match{ Name="FCB-Madrid", HouseTeamManagerId=managers[0].Id, AwayTeamManagerId=managers[2].Id, RefereeId=referees[0].Id },
                new Match{ Name="FCB-Madrid", HouseTeamManagerId=managers[1].Id, AwayTeamManagerId=managers[2].Id, RefereeId=referees[2].Id },
                new Match{ Name="Madrid-FCB", HouseTeamManagerId=managers[2].Id, AwayTeamManagerId=managers[1].Id, RefereeId=referees[1].Id },
                new Match{ Name="FCB-Madrid", HouseTeamManagerId=managers[1].Id, AwayTeamManagerId=managers[3].Id, RefereeId=referees[0].Id },

            };

            foreach (Match match in matches)
            {
                context.Match.Add(match);
            }
            context.SaveChanges();


            var homeMatches = new MatchPlayersHouse[]
            {
                new MatchPlayersHouse{ MatchId=matches[0].Id, PlayerId=players[0].Id},
                new MatchPlayersHouse{ MatchId=matches[0].Id, PlayerId=players[1].Id},
                new MatchPlayersHouse{ MatchId=matches[0].Id, PlayerId=players[2].Id},
                new MatchPlayersHouse{ MatchId=matches[0].Id, PlayerId=players[3].Id},
                new MatchPlayersHouse{ MatchId=matches[0].Id, PlayerId=players[4].Id},

                new MatchPlayersHouse{ MatchId=matches[1].Id, PlayerId=players[0].Id},
                new MatchPlayersHouse{ MatchId=matches[1].Id, PlayerId=players[1].Id},
                new MatchPlayersHouse{ MatchId=matches[1].Id, PlayerId=players[2].Id},
                new MatchPlayersHouse{ MatchId=matches[1].Id, PlayerId=players[3].Id},
                new MatchPlayersHouse{ MatchId=matches[1].Id, PlayerId=players[4].Id},

                new MatchPlayersHouse{ MatchId=matches[2].Id, PlayerId=players[6].Id},
                new MatchPlayersHouse{ MatchId=matches[2].Id, PlayerId=players[7].Id},
            };

            foreach (MatchPlayersHouse homeMatche in homeMatches)
            {
                var matchInDataBase = context.MatchPlayersHouse.Where(
                            s =>
                            s.Match.Id == homeMatche.MatchId &&
                            s.Player.Id == homeMatche.PlayerId
                ).SingleOrDefault();

                if (matchInDataBase == null)
                {
                    context.MatchPlayersHouse.Add(homeMatche);
                }
            }
            context.SaveChanges();


            var awayMatches = new MatchPlayersAway[]
            {
                new MatchPlayersAway{ MatchId=matches[0].Id, PlayerId=players[6].Id},
                new MatchPlayersAway{ MatchId=matches[0].Id, PlayerId=players[7].Id},

                new MatchPlayersAway{ MatchId=matches[1].Id, PlayerId=players[6].Id},
                new MatchPlayersAway{ MatchId=matches[1].Id, PlayerId=players[7].Id},

                new MatchPlayersAway{ MatchId=matches[2].Id, PlayerId=players[0].Id},
                new MatchPlayersAway{ MatchId=matches[2].Id, PlayerId=players[1].Id},
                new MatchPlayersAway{ MatchId=matches[2].Id, PlayerId=players[2].Id},
                new MatchPlayersAway{ MatchId=matches[2].Id, PlayerId=players[3].Id},
                new MatchPlayersAway{ MatchId=matches[2].Id, PlayerId=players[4].Id},
                new MatchPlayersAway{ MatchId=matches[2].Id, PlayerId=players[5].Id},
            };

            foreach (MatchPlayersAway awayMatche in awayMatches)
            {
                var matchInDataBase = context.MatchPlayersAway.Where(
                            s =>
                            s.Match.Id == awayMatche.MatchId &&
                            s.Player.Id == awayMatche.PlayerId
                ).SingleOrDefault();

                if (matchInDataBase == null)
                {
                    context.MatchPlayersAway.Add(awayMatche);
                }
            }
            context.SaveChanges();


        }
    }
}
