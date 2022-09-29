using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShipperId { get; set; }
        public int LocationId { get; set; }
        public int SlotId { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public virtual Location Location { get; set; } = null!;
        public virtual Shipper Shipper { get; set; } = null!;
        public virtual Slot Slot { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
