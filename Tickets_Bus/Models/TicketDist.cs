using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class TicketDist
    {
        [Display(Name = "Код Квитка")]
        public int ID_Ticket { get; set; }
        [Display(Name = "Код Рейса")]
        public int ID_Route { get; set; }
        [Display(Name = "Пункт відправлення")]
        public int Departure { get; set; }
        [Display(Name = "Пункт прибуття")]
        public int Arrival { get; set; }
        [Display(Name = "Місце посадки")]
        public int? Numb_Seat { get; set; }
        [Display(Name = "Ціна")]
        public double Price { get; set; }
        [Display(Name = "ПІБ")]
        public string Name_Surname { get; set; }
        [Display(Name = "Дата відправлення")]
        public System.DateTime Date_Sale { get; set; }

        public virtual Route_ Route_ { get; set; }
        public virtual Station Station { get; set; }
        public virtual Station Station1 { get; set; }
    }
}