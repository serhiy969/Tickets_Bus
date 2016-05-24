using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class RouteStInfo
    {
        [Display(Name = "Код Станції")]
        public int ID_Station { get; set; }
        [Display(Name = "Код Рейсу")]
        public int ID_Route { get; set; }
        [Display(Name = "Час відправлення")]
        public System.TimeSpan Date_departure { get; set; }
        [Display(Name = "Час прибуття")]
        public System.TimeSpan Date_arrival { get; set; }
        [Display(Name = "Відстань")]
        public int Distance { get; set; }
        [Display(Name = "Місце посадки")]
        public int Numb_Seat { get; set; }
        [Display(Name = "Місце посадки")]
        public int Num_Seats { get; set; }
    }
}