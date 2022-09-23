using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public partial class Shift
    {
        public int SlotId { get; set; }
        public int ShipperId { get; set; }
        public bool Status { get; set; }

        public virtual Shipper Shipper { get; set; } = null!;
        public virtual Slot Slot { get; set; } = null!;
    }
}
