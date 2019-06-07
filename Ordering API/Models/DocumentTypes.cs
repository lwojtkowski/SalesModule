using System;
using System.Collections.Generic;

namespace Ordering_API.Models
{
    public partial class DocumentTypes
    {
        public DocumentTypes()
        {
            SalesDocuments = new HashSet<SalesDocuments>();
        }

        public int DocumentTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<SalesDocuments> SalesDocuments { get; set; }
    }
}
