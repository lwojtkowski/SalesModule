using System;
using System.Collections.Generic;

namespace Catalog_API.Models
{
    public partial class ProductsWarehouses
    {
        public int Warehouses { get; set; }
        public int Products { get; set; }
        public int Quantity { get; set; }

        public virtual Products ProductsNavigation { get; set; }
        public virtual Warehouses WarehousesNavigation { get; set; }
    }
}
