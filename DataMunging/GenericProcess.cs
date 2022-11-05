using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunging
{
    public class GenericProcess<T>
    {
        private T GenericField;
        public T GenericProcessMethod(string path)
        {
            WeatherData weatherResult = new WeatherData();
            SoccerLeagueTableDto soccerLeagueTable = new SoccerLeagueTableDto();
            var entityName = typeof(T).Name;
            if (entityName == "WeatherData")
            {
                var result = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(TransformToWeatherData)
                .ToList();

                foreach (var weather in result.Where(x => x.MinTemp.Contains("*")))
                {
                    weather.MinTemp = weather.MinTemp.TrimEnd('*');
                }

                var minWeather = result.Min(w => double.Parse(w.MinTemp));
                weatherResult = result.First(x => double.Parse(x.MinTemp) == minWeather);
                GenericField = (T)Convert.ChangeType(weatherResult, typeof(T));
            }
            else if (entityName == "SoccerLeagueTableDto")
            {
                var result = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(TransformToSoccerLeagueData)
                .ToList();

                var soccerGoalsResult = result.Select(x => x.GoalsFor - x.GoalsAgainst).Min();

                var resd = result.Select(x => new SoccerLeagueTableDto { Team = x.Team, GoalDiff = x.GoalsFor - x.GoalsAgainst }).ToList();

                var finalList = resd.Where(x => x.GoalDiff == soccerGoalsResult).First();

                GenericField = (T)Convert.ChangeType(finalList, typeof(T));
            }
            return GenericField;
        }

        private static WeatherData TransformToWeatherData(string line)
        {
            var columns = line.Split(' ');
            var res = columns.Where(n => n != string.Empty).ToList();
            var weather = new WeatherData();
            var result = new WeatherData
            {
                Day = res[0],
                MaxTemp = res[1],
                MinTemp = res[2]
            };
            weather = result;
            return weather;
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
