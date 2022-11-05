using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunging
{
    public class SoccerLeagueTable
    {
        public string Team { get; set; }
        public int Played { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public int Draw { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int TotalPoints { get; set; }
    }
}
