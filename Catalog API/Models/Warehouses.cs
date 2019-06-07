using System;
using System.Collections.Generic;

namespace Catalog_API.Models
{
    public partial class Warehouses
    {
        public Warehouses()
        {
            ProductsWarehouses = new HashSet<ProductsWarehouses>();
        }

        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public virtual ICollection<ProductsWarehouses> ProductsWarehouses { get; set; }
    }
}
