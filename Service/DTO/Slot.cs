using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public partial class Slot
    {
        public Slot()
        {
            Orders = new HashSet<Order>();
            Shifts = new HashSet<Shift>();
        }

        public int Id { get; set; }
        public int MenuId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public bool? Status { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
