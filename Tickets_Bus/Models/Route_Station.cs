//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tickets_Bus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Route_Station
    {
        public int ID_Station { get; set; }
        public int ID_Route { get; set; }
        public System.TimeSpan Date_departure { get; set; }
        public System.TimeSpan Date_arrival { get; set; }
        public int Distance { get; set; }
        public Nullable<int> Numof_Order { get; set; }
        public DateTime ID_Date_Route { get; set; }
    
        public virtual Route_ Route_ { get; set; }
        public virtual Station Station { get; set; }
    }
}
