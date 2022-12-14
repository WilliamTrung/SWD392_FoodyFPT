using System;
using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuDetails = new HashSet<MenuDetail>();
            Slots = new HashSet<Slot>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
        public virtual ICollection<Slot>? Slots { get; set; }
    }
}
