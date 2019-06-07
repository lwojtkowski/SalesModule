using System;
using System.Collections.Generic;

namespace Catalog_API.Models
{
    public partial class ProductTypes
    {
        public ProductTypes()
        {
            Products = new HashSet<Products>();
        }

        public int ProductTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
