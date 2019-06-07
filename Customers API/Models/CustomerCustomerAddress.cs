using System;
using System.Collections.Generic;

namespace Customers_API.Models
{
    public partial class CustomerCustomerAddress
    {
        public int Customer { get; set; }
        public int CustomerAddress { get; set; }

        public virtual CustomerAddress CustomerAddressNavigation { get; set; }
        public virtual Customer CustomerNavigation { get; set; }
    }
}
