using System;
using System.Collections.Generic;

namespace Customers_API.Models
{
    public partial class CustomerAddress
    {
        public CustomerAddress()
        {
            CustomerCustomerAddress = new HashSet<CustomerCustomerAddress>();
        }

        public int CustomerAddressId { get; set; }
        public string Province { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }

        public virtual ICollection<CustomerCustomerAddress> CustomerCustomerAddress { get; set; }
    }
}
