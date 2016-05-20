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
        public Nullable<int> Num_Seats { get; set; }
        public System.DateTime Date_LastTO { get; set; }
        public string Reliability { get; set; }
     
    }
}