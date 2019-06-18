using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Customers_API.Models;

namespace Customers_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomersDBContext _context;

        public CustomerController(CustomersDBContext context)
        {
            _context = context;
        }

        // GET: api/Customer/5
        [HttpGet("customer/{email},{name},{surname},{phoneNumber}")]
        public async Task<ActionResult<CustomerAddressModel>> GetCustomerAddress(string email, string name, string surname, string phoneNumber)
        {
			var _customerAddressModel = new CustomerAddressModel();
			var addressList = new List<CustomerAddressData>();

			var customerData = await _context.Customer.Where(p => p.Email == email
															 || p.Name == name
															 && p.Surname == surname
															 && p.PhoneNumber == phoneNumber)
													  .Select(p => new CustomerData
													  {
														  CustomerID = p.CustomerId,
														  Name = p.Name,
														  Surname = p.Surname,
														  Email = p.Email,
														  PhoneNumber = p.PhoneNumber
													  })	
													  .FirstAsync();

			var customerAddress = await _context.CustomerCustomerAddress.Where(p => p.Customer == customerData.CustomerID)
																		.Select(p => p.CustomerAddress)
															            .ToListAsync();

			foreach (var address in customerAddress)
			{
				var addressTemp = await _context.CustomerAddress.Where(p => p.CustomerAddressId == address)
																.Select(p => new CustomerAddressData
																{
																	CustomerAddressId = p.CustomerAddressId,
																	Province = p.Province,
																	Zipcode = p.Zipcode,
																	City = p.City,
																	Street = p.Street,
																	BuildingNumber = p.BuildingNumber,
																	ApartmentNumber = p.ApartmentNumber,
																})
																.FirstAsync();
				addressList.Add(addressTemp);
			}

			_customerAddressModel.CustomerData = customerData;
			_customerAddressModel.CustomerAddressData = addressList;

			return _customerAddressModel;
		}

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerCustomerAddress>> PostCustomerCustomerAddress(CustomerCustomerAddress customerCustomerAddress)
        {
            _context.CustomerCustomerAddress.Add(customerCustomerAddress);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerCustomerAddressExists(customerCustomerAddress.Customer))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerCustomerAddress", new { id = customerCustomerAddress.Customer }, customerCustomerAddress);
        }

        private bool CustomerCustomerAddressExists(int id)
        {
            return _context.CustomerCustomerAddress.Any(e => e.Customer == id);
        }
    }
}
