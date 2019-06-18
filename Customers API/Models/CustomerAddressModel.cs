using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers_API.Models
{
	public class CustomerAddressModel
	{
		public CustomerData CustomerData { get; set; }
		public List<CustomerAddressData> CustomerAddressData { get; set; }
	}

	public class CustomerData
	{
		public int CustomerID { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}

	public class CustomerAddressData
	{
		public int CustomerAddressId { get; set; }
		public string Province { get; set; }
		public string Zipcode { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string BuildingNumber { get; set; }
		public string ApartmentNumber { get; set; }
	}
}
