using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering_API.Models
{
	public class OrderModel
	{
		public int DocumentType { get; set; }
		public int MyProperty { get; set; }
		public CustomerTemp CustomerData { get; set; }
		public AddressTemp AddressData { get; set; }
		public int WarehouseID { get; set; }
		public int DocumentNumber { get; set; }
		public int BasketID { get; set; }
		public int UserID { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime OrderDate { get; set; }
		public ProductTemp[] Products { get; set; }
	}

	public class CustomerTemp
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
	}

	public class AddressTemp
	{
		public int AddressId { get; set; }
		public string Province { get; set; }
		public string Zipcode { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string BuildingNumber { get; set; }
		public string ApartmentNumber { get; set; }
	}

	public class ProductTemp
	{
		public int ProductId { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
		public int Punctation { get; set; }
	}
}
