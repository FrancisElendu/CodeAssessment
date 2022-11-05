using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunging
{
    public class SoccerLeagueTableDto
    {
        public string Team { get; set; }
        public int GoalDiff { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
    }
}
