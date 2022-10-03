using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
            Shifts = new HashSet<Shift>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public long Salary { get; set; }
        public bool? Status { get; set; }

        public virtual User? User { get; set; } = null;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
