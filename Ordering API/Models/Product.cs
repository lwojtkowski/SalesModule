using System;
using System.Collections.Generic;

namespace Ordering_API.Models
{
    public partial class Product
    {
        public Product()
        {
            SalesDocumentsProduct = new HashSet<SalesDocumentsProduct>();
        }

        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int Punctation { get; set; }

        public virtual ICollection<SalesDocumentsProduct> SalesDocumentsProduct { get; set; }
    }
}
