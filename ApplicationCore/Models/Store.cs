using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public partial class Store
    {
        public Store()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public bool? Flag { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
