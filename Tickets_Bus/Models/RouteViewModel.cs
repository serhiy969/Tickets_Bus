using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class RouteViewModel
    {
        [Display(Name = "Код Рейсу")]
        public int ID_Route { get; set; }
        [Display(Name = "Час відправлення")]
        public TimeSpan Date_Departure { get; set; }
        [Display(Name = "Пункт відправлення")]
        public int Daeparture { get; set; }
        [Display(Name = "Пункт прибуття")]
        public int Arrival { get; set; }
        [Display(Name = "Код Станції")]
        public int ID_Station { get; set; }
        [Display(Name = "Час прибуття")]
        public TimeSpan Date_Arrival { get; set; }

        public Route_ Route { get; set; }

        public Route_Station RouteStation { get; set; }
        public Station Station { get; set; }
        public BUS Bus { get; set; }
        public Driver DRivers { get; set; }

        public DateTime Date_Route { get; set; }
        [Display(Name = "Пункт відправлення")]
        public string Station1 { get; set; }
        [Display(Name = "Пункт прибуття")]
        public string Station2 { get; set; }
        [Display(Name = "Пункт прибуття")]
        public string Station3 { get; set; }
        [Display(Name = "Відстань")]
        public int Distance_ { get; set; }
        [Display(Name = "Надійність")]
        public string Reliability_ { get; set; }
        public string StationD { get; set; }
        public string StationA { get; set; }
        [Display(Name = "Назва атобуса")]
        public string Name_buses { get; set; }
        [Display(Name = "Дата прибуття")]
        public DateTime DateArrival { get; set; }
    }
}