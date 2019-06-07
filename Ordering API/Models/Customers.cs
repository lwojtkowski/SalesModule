using System;
using System.Collections.Generic;

namespace Ordering_API.Models
{
    public partial class Customers
    {
        public Customers()
        {
            SalesDocuments = new HashSet<SalesDocuments>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual ICollection<SalesDocuments> SalesDocuments { get; set; }
    }
}
