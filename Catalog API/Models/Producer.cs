using System;
using System.Collections.Generic;

namespace Catalog_API.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Products = new HashSet<Products>();
        }

        public int ProducerId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
