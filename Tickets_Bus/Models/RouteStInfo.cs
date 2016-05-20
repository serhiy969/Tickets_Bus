using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class RouteStInfo
    {
        public int ID_Station { get; set; }
        public int ID_Route { get; set; }
        public System.TimeSpan Date_departure { get; set; }
        public System.TimeSpan Date_arrival { get; set; }
        public int Distance { get; set; }

        public int Numb_Seat { get; set; }
        public int Num_Seats { get; set; }
    }
}