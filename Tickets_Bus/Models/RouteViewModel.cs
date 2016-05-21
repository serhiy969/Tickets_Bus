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

        public DateTime Date_Route { get; set; }
    }
}