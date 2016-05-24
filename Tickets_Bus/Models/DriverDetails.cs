using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tickets_Bus.Models
{
    public class DriverDetails
    {
        [Display(Name = "Код атобуса")]
        public int ID_Bus { get; set; }
        [Display(Name = "Назва атобуса")]
        public string Name_Bus { get; set; }
        [Display(Name = "Номер атобуса")]
        public string Number_Bus { get; set; }
        [Display(Name = "Кількість місць")]
        public int Num_Seats { get; set; }
        [Display(Name = "Дата ТО")]
        public System.DateTime Date_LastTO { get; set; }
        [Display(Name = "Надійність")]
        public string Reliability { get; set; }

        [Display(Name = "Код водія")]
        public int ID_Driver { get; set; }
        [Display(Name = "ПІБ")]
        public string FirstLastName { get; set; }
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Display(Name = "Місце посадки")]
        public int? NumSeats { get; set; }
    }
}