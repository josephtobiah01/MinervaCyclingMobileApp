using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Models
{
    public class DirectionsResponse
    {
        public string Code { get; set; }

        public Waypoint[] Waypoints { get; set; }

        public Route[] Routes { get; set; }
    }

    public class Waypoint
    {
        public string Name { get; set; }

        public double[] Location { get; set; }
    }

    public class Route
    {
        public string Geometry { get; set; }

        public Waypoint[] Waypoints { get; set; }

        public Leg[] Legs { get; set; }
    }

    public class Leg
    {
        public string Summary { get; set; }

        public double Weight { get; set; }

        public double Duration { get; set; }

        public Step[] Steps { get; set; }
    }

    public class Step
    {
        public Intersection[] Intersections { get; set; }

        public string DrivingSide { get; set; }

        public string Geometry { get; set; }

        public string Mode { get; set; }

        public Maneuver Maneuver { get; set; }

        public string Ref { get; set; }

        public double Weight { get; set; }

        public double Duration { get; set; }

        public string Name { get; set; }

        public double Distance { get; set; }
    }

    public class Intersection
    {
        public int Out { get; set; }
        
        public bool[] Entry { get; set; }
        
        public int[] Bearings { get; set; }

        public double[] Location { get; set; }
    }

    public class Maneuver
    {
        public int BearingAfter { get; set; }

        public int BearingBefore { get; set; }

        public double[] Location { get; set; }

        public string Modifier { get; set; }

        public string Type { get; set; }

        public string Instruction { get; set; }
    }

}
