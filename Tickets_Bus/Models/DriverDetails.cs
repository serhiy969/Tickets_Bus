using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class DriverDetails
    {
        public int ID_Bus { get; set; }
        public string Name_Bus { get; set; }
        public string Number_Bus { get; set; }
        public int? Num_Seats { get; set; }
        public System.DateTime Date_LastTO { get; set; }
        public string Reliability { get; set; }

        public int ID_Driver { get; set; }
        public string FirstLastName { get; set; }
        public int ID_bus { get; set; }
        public int? NumSeats { get; set; }
    }
}