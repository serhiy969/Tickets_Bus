//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Tickets_Bus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Route_Station
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
        [Display(Name = "Проміжна Станція")]
        public Nullable<int> Numof_Order { get; set; }
        [Display(Name = "Дата рейса")]
        public DateTime ID_Date_Route { get; set; }
    
        public virtual Route_ Route_ { get; set; }
        public virtual Station Station { get; set; }
    }
}
