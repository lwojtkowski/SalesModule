﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering_API.Models;

namespace Ordering_API.Controllers
{
	[Route("api")]
	[ApiController]
	public class OrderingController : ControllerBase
	{
		private readonly OrderingDBContext _context;
		private readonly OrderingDBContext _context2;

		public OrderingController(OrderingDBContext context)
		{
			_context = context;
		}

		// GET: api/Ordering
		[HttpGet]
		public async Task<ActionResult<IEnumerable<SalesDocumentsProduct>>> GetSalesDocumentsProduct()
		{
			return await _context.SalesDocumentsProduct.ToListAsync();
		}

		// GET: api/Ordering/5
		[HttpGet("{id}")]
		public async Task<ActionResult<SalesDocumentsProduct>> GetSalesDocumentsProduct(int id)
		{
			var salesDocumentsProduct = await _context.SalesDocumentsProduct.FindAsync(id);

			if (salesDocumentsProduct == null)
			{
				return NotFound();
			}

			return salesDocumentsProduct;
		}

		// POST: api/createDocument/
		[EnableCors("CorsPolicy")]
		[HttpPost("createDocument/")]
		public async Task<ActionResult<object>> PostSalesDocument(OrderModel order)
		{
			if (order == null)
			{
				return NotFound();
			};

			var _salesDocuments = new SalesDocuments();
			var _customer = new Customers();
			var _address = new Address();
			var _basket = new SalesDocumentsProduct();
			var _product = new Product();
			var _productTemp = new ProductTemp();

			if (order.CustomerData != null)
			{
				_customer.Name = order.CustomerData.Name;
				_customer.Surname = order.CustomerData.Surname;
				_customer.PhoneNumber = order.CustomerData.PhoneNumber;
				_customer.Email = order.CustomerData.Email;
			}

			_context.Customers.Add(_customer);
			

			if (order.AddressData != null)
			{
				_address.Zipcode = order.AddressData.Zipcode;
				_address.City = order.AddressData.City;
				_address.Street = order.AddressData.Street;
				_address.BuildingNumber = order.AddressData.BuildingNumber;
				_address.ApartmentNumber = order.AddressData.ApartmentNumber;
			}

			_context.Address.Add(_address);

			await _context.SaveChangesAsync();

			int documentNumber =  await _context.SalesDocuments.Where(p => p.WarehouseId == order.WarehouseID && p.DocumentTypes == order.DocumentType)
															   .Select(p => p.DocumentNumber)
															   .LastAsync();

			_salesDocuments.Customer = _customer.CustomerId;
			_salesDocuments.CustomerAddress = _address.AddressId;
			_salesDocuments.DocumentTypes = order.DocumentType;
			_salesDocuments.WarehouseId = order.WarehouseID;
			_salesDocuments.DocumentNumber = documentNumber + 1;
			_salesDocuments.BasketId = order.BasketID;
			_salesDocuments.UserId = order.UserID;
			_salesDocuments.CreationDate = DateTime.Now;
			_salesDocuments.OrderDate = DateTime.Now;

			_context.SalesDocuments.Add(_salesDocuments);

			await _context.SaveChangesAsync();


			foreach (var product in order.Products)
			{
				var productID = await _context.Product.Where(p => p.ProductId == product.ProductId)
													  .Select(p => p.ProductId)
													  .AnyAsync();
				if (productID == true)
				{
					_basket.SalesDocuments = _salesDocuments.SalesDocumentId;
					_basket.Product = product.ProductId;

					_context.SalesDocumentsProduct.Add(_basket);
					await _context.SaveChangesAsync();
				}
				else
				{					
					_product.ProductId = product.ProductId;
					_product.Description = product.Description;
					_product.Quantity = product.Quantity;
					_product.Price = product.Price;
					_product.Punctation = product.Punctation;

					_context.Product.Add(_product);

					_context.Database.OpenConnection();
					try
					{
						_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Product ON");
						await _context.SaveChangesAsync();
						_context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Product OFF");
					}
					finally
					{
						_context.Database.CloseConnection();
					}

					_basket.SalesDocuments = _salesDocuments.SalesDocumentId;
					_basket.Product = product.ProductId;

					_context.SalesDocumentsProduct.Add(_basket);
					await _context.SaveChangesAsync();
				}
			}

			return Ok($"Document created: {_salesDocuments.SalesDocumentId}");
		}

		// DELETE: api/Ordering/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<SalesDocumentsProduct>> DeleteSalesDocumentsProduct(int id)
		{
			var salesDocumentsProduct = await _context.SalesDocumentsProduct.FindAsync(id);
			if (salesDocumentsProduct == null)
			{
				return NotFound();
			}

			_context.SalesDocumentsProduct.Remove(salesDocumentsProduct);
			await _context.SaveChangesAsync();

			return salesDocumentsProduct;
		}

		//private bool SalesDocumentsExists(int id)
		//{
		//    return _context.SalesDocuments.Any(e => e.SalesDocuments == id);
		//}
	}

	public class OrderModel
	{
		public int DocumentType { get; set; }
		public CustomerTemp CustomerData { get; set; }
		public AddressTemp AddressData { get; set; }
		public int WarehouseID { get; set; }
		public int BasketID { get; set; }
		public int UserID { get; set; }
		public string OrderDate { get; set; }
		public List<ProductTemp> Products { get; set; }
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