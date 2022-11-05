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
    }
}
