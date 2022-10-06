using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public partial class MenuDetail
    {
        public int ProductId { get; set; }
        public int MenuId { get; set; }
        public bool Status { get; set; }

        //public virtual Menu? Menu { get; set; } = null;
        public virtual Product? Product { get; set; } = null;
    }
}
