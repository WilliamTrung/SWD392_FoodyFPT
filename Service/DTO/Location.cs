using System;
using System.Collections.Generic;


namespace Service.DTO
{
    public partial class Location
    {
        public Location()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Floor { get; set; }
        public bool? Flag { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
