using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class RouteViewModel
    {
        public int ID_Route { get; set; }
        public TimeSpan Date_Departure { get; set; }

        public int Daeparture { get; set; }
        public int Arrival { get; set; }
        public int ID_Station { get; set; }
        public TimeSpan Date_Arrival { get; set; }

        public Route_ Route { get; set; }

        public Route_Station RouteStation { get; set; }
        public Station Station { get; set; }
        public BUS Bus { get; set; }
        public Driver DRivers { get; set; }

        public DateTime Date_Route { get; set; }
        public string Station1 { get; set; }
        public string Station2 { get; set; }
        public string Station3 { get; set; }
        public int Distance_ { get; set; }
        public string Reliability_ { get; set; }
        public string StationD { get; set; }
        public string StationA { get; set; }
        public string Name_buses { get; set; }
    }
}