using System;
using System.Collections.Generic;

namespace Ordering_API.Models
{
    public partial class SalesDocuments
    {
        public SalesDocuments()
        {
            SalesDocumentsProduct = new HashSet<SalesDocumentsProduct>();
        }

        public int SalesDocumentId { get; set; }
        public int DocumentTypes { get; set; }
        public int? Customer { get; set; }
        public int? CustomerAddress { get; set; }
        public int WarehouseId { get; set; }
        public int DocumentNumber { get; set; }
        public int BasketId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Address CustomerAddressNavigation { get; set; }
        public virtual Customers CustomerNavigation { get; set; }
        public virtual DocumentTypes DocumentTypesNavigation { get; set; }
        public virtual ICollection<SalesDocumentsProduct> SalesDocumentsProduct { get; set; }
    }
}
