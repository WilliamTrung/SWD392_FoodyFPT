using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Shippers = new HashSet<Shipper>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Image { get; set; } = null!;

        public virtual Role? Role { get; set; } = null;
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Shipper>? Shippers { get; set; }
    }
}
