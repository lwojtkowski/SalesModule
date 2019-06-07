using System;
using System.Collections.Generic;

namespace Ordering_API.Models
{
    public partial class SalesDocumentsProduct
    {
        public int SalesDocuments { get; set; }
        public int Product { get; set; }

        public virtual Product ProductNavigation { get; set; }
        public virtual SalesDocuments SalesDocumentsNavigation { get; set; }
    }
}
