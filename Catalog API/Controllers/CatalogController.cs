using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace Catalog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
		private readonly CatalogDBContext _context;

		public CatalogController(CatalogDBContext context)
		{
			_context = context;
		}
		//Tes
		// GET: api/Catalog/products/{ID}
		[EnableCors("CorsPolicy")]
		[HttpGet("products/{id}")]
		public async Task<ActionResult<object>> GetProducts(int id)
		{
			var products = await _context.Products.Where(p => p.ProductId == id)
												.Select(p => new
												{
													productID = p.ProductId,
													productType = p.ProductTypeNavigation.Type,
													descriptionImage = p.DescriptionImage,
													description = p.Description,
													price = p.Price
												})
												.FirstAsync();

			if (products == null)
			{
				return NotFound();
			}
			return products;
		}
		[EnableCors("CorsPolicy")]
		[HttpGet("aviability/{warehouseID},{productID}")]
		public async Task<ActionResult<object>> GetAviability(int warehouseID, int productID)
		{
			var aviability = await _context.ProductsWarehouses.Where(p => p.Products == productID && p.Warehouses == warehouseID)
												.Select(p => new
												{
													productID = p.Products,
													warehouseID = p.Warehouses,
													quantity = p.Quantity
												})
												.FirstAsync();
			if (aviability == null)
			{
				return NotFound();
			}
			return aviability;
		}
	}
}
