using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Models
{
    public class MapMatchResponse
    {
        public List<Matching> Matchings { get; set; }
        public List<Tracepoint> Tracepoints { get; set; }
    }

    public class Matching
    {
        public double Confidence { get; set;}
        public Geometry Geometry { get; set;}
    }

    public class Geometry
    {
        public List<List<double>> Coordinates { get; set; }
        public string Type { get; set; } 
    }

    public class Tracepoint
    {
        public List<double> Location { get; set; }
        public string Name { get; set; }

    }


}
