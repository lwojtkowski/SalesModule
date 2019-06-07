using System;
using System.Collections.Generic;

namespace Customers_API.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerCustomerAddress = new HashSet<CustomerCustomerAddress>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int AddressId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<CustomerCustomerAddress> CustomerCustomerAddress { get; set; }
    }
}
