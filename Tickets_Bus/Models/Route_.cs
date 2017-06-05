//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Tickets_Bus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Route_ : IEnumerable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route_()
        {
            this.Route_Station = new HashSet<Route_Station>();
            this.Tickets = new HashSet<Ticket>();
        }
        [Display(Name = "Код Рейсу")]
        public int ID_Route { get; set; }
        [Display(Name = "Пункт відправлення")]
        public int Departure { get; set; }
        [Display(Name = "Пункт прибуття")]
        public int Arrival { get; set; }
        [Display(Name = "Час відправлення")]
        public TimeSpan Date_departure { get; set; }
        [Display(Name = "Час прибуття")]
        public TimeSpan Date_arrival { get; set; }
        [Display(Name = "Код водія")]
        public int ID_Driver { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата відправлення")]
        public DateTime DateArrival { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Station Station { get; set; }
        public virtual Station Station1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Route_Station> Route_Station { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
