using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public partial class Product
    {
        public Product()
        {
            MenuDetails = new HashSet<MenuDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }

        public virtual Category? Category { get; set; } = null;
        public virtual Store? Store { get; set; } = null;
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
