using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class TicketDist
    {
        public int ID_Ticket { get; set; }
        public int ID_Route { get; set; }
        public int Departure { get; set; }
        public int Arrival { get; set; }
        public Nullable<int> Numb_Seat { get; set; }
        public double Price { get; set; }
        public string Name_Surname { get; set; }
        public System.DateTime Date_Sale { get; set; }

        public virtual Route_ Route_ { get; set; }
        public virtual Station Station { get; set; }
        public virtual Station Station1 { get; set; }
    }
}