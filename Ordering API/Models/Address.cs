using System;
using System.Collections.Generic;

namespace Ordering_API.Models
{
    public partial class Address
    {
        public Address()
        {
            SalesDocuments = new HashSet<SalesDocuments>();
        }

        public int AddressId { get; set; }
        public string Province { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }

        public virtual ICollection<SalesDocuments> SalesDocuments { get; set; }
    }
}
