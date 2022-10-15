using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.View
{
    public class ProductView
    {
        public IEnumerable<Product> products;
        public ProductView(IEnumerable<Product> products)
        {
            this.products = products;
        }
    }
}
