using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunging
{
    public class SoccerLeagueReport
    {
        public SoccerLeagueTableDto SoccerLeagueProcess(string path)
        {
            var result = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(TransformToSoccerLeagueData)
                .ToList();

            var soccerGoalsResult = result.Select(x => x.GoalsFor - x.GoalsAgainst).Min();

            var resd = result.Select(x => new SoccerLeagueTableDto { Team = x.Team, GoalDiff = x.GoalsFor - x.GoalsAgainst }).ToList();

            var finalList = resd.Where(x => x.GoalDiff == soccerGoalsResult).First();

            return finalList;
        }

        private static SoccerLeagueTableDto TransformToSoccerLeagueData(string line)
        {
            var soccerTable = new SoccerLeagueTable();
            var soccerTableDto = new SoccerLeagueTableDto();
            var columns = line.Split(' ');
            var res = columns.Where(n => n != string.Empty).ToList();
            if (!res[0].StartsWith("-"))
            {
                var result = new SoccerLeagueTable
                {
                    Team = res[1],
                    Played = int.Parse(res[2]),
                    Win = int.Parse(res[3]),
                    Loss = int.Parse(res[4]),
                    Draw = int.Parse(res[5]),
                    GoalsFor = int.Parse(res[6]),
                    GoalsAgainst = int.Parse(res[8]),
                    TotalPoints = int.Parse(res[9])
                };
                soccerTableDto = new SoccerLeagueTableDto { Team = result.Team, GoalsFor = result.GoalsFor, GoalsAgainst = result.GoalsAgainst };
            }
            return soccerTableDto;
        }
    }
}
