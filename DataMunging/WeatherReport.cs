using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunging
{
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
}
