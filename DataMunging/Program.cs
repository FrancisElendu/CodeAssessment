using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataMunging
{
    class Program
    {
        static void Main(string[] args)
        {

            //Non Generic

            WeatherReport weatherReport = new WeatherReport();
            SoccerLeagueReport soccerLeagueReport = new SoccerLeagueReport();

            var soccer = soccerLeagueReport.SoccerLeagueProcess("football.txt");


            var weather = weatherReport.WeatherProcessFile("weather.txt");

            Console.WriteLine($"Final output for Non-generic weather report- Day: {weather.Day} and Minimum Temperature: {weather.MinTemp}");

            Console.WriteLine($"Final output for Non-generic soccer league report- Team: {soccer.Team} and Goals difference: {soccer.GoalDiff}");
            Console.WriteLine($"=======================================================================");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //Using Generics

            GenericProcess<WeatherData> weatherProcess = new GenericProcess<WeatherData>();

            var genericWeather = weatherProcess.GenericProcessMethod("weather.txt");

            GenericProcess<SoccerLeagueTableDto> soccerProcess = new GenericProcess<SoccerLeagueTableDto>();

            var genericSoccer = soccerProcess.GenericProcessMethod("football.txt");


            Console.WriteLine($"Final output for Generic weather report- Day: {genericWeather.Day} and Minimum Temperature: {genericWeather.MinTemp}");


            Console.WriteLine($"Final output for Generic soccer league report- Team: {genericSoccer.Team} and Goals difference: {genericSoccer.GoalDiff}");
            Console.WriteLine($"***********************************************************************");

            Console.ReadLine();

        }

        //private static List<WeatherData> ProcessFile (string path)
        //{
        //    return File.ReadAllLines(path)
        //        .Skip(1)
        //        .Where(line => line.Length > 1)
        //        .Select(TransformToWeather)
        //        .ToList();
        //}

        //private static WeatherData TransformToWeather(string line)
        //{
        //    var columns = line.Split(' ');
        //    var res = columns.Where(n => n != string.Empty).ToList();
        //    var weather = new WeatherData();
        //    var result = new WeatherData
        //    {
        //        Day = res[0],
        //        MaxTemp = res[1],
        //        MinTemp = res[2]
        //    };
        //    weather = result;
        //    return weather;
        //}

        //private static List<SoccerLeagueTable> ProcessSoccerLeague(string path)
        //{
        //    return File.ReadAllLines(path)
        //        .Skip(1)
        //        .Where(line => line.Length > 1)
        //        .Select(TransformToSoccerLeague)
        //        .ToList();
        //}

        //private static SoccerLeagueTable TransformToSoccerLeague(string line)
        //{
        //    var soccerTable = new SoccerLeagueTable();
        //    var columns = line.Split(' ');
        //    var res = columns.Where(n => n != string.Empty).ToList();
        //    if (!res[0].StartsWith("-"))
        //    {
        //        var result = new SoccerLeagueTable
        //        {
        //            Team = res[1],
        //            Played = int.Parse(res[2]),
        //            Win = int.Parse(res[3]),
        //            Loss = int.Parse(res[4]),
        //            Draw = int.Parse(res[5]),
        //            GoalsFor = int.Parse(res[6]),
        //            GoalsAgainst = int.Parse(res[8]),
        //            TotalPoints = int.Parse(res[9])
        //        };
        //        soccerTable = result;
        //    }
        //    return soccerTable;
        //}
    }


    public class WeatherReport
    {
        public WeatherData WeatherProcessFile(string path)
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
            var weatherResult = result.First(x => double.Parse(x.MinTemp) == minWeather);
            return weatherResult;
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

    }


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

    public class GenericProcess<T>
    {
        private T GenericField;
        public T GenericProcessMethod(string path)
        {
            WeatherData weatherResult = new WeatherData();
            SoccerLeagueTableDto soccerLeagueTable = new SoccerLeagueTableDto();
            var entityName = typeof(T).Name;
            if(entityName== "WeatherData")
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
                GenericField= (T)Convert.ChangeType(weatherResult, typeof(T));
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
                soccerTableDto = new SoccerLeagueTableDto { Team  = result.Team, GoalsFor = result.GoalsFor, GoalsAgainst = result.GoalsAgainst};
            }
            return soccerTableDto;
        }
    }

}
