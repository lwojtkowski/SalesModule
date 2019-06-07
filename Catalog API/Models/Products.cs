using System;
using System.Collections.Generic;

namespace Catalog_API.Models
{
    public partial class Products
    {
        public Products()
        {
            ProductsWarehouses = new HashSet<ProductsWarehouses>();
        }

        public int ProductId { get; set; }
        public int Producer { get; set; }
        public int ProductType { get; set; }
        public string DescriptionImage { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public string Ean { get; set; }
        public float Price { get; set; }
        public int VatTax { get; set; }
        public int Punctation { get; set; }

        public virtual Producer ProducerNavigation { get; set; }
        public virtual ProductTypes ProductTypeNavigation { get; set; }
        public virtual ICollection<ProductsWarehouses> ProductsWarehouses { get; set; }
    }
}
