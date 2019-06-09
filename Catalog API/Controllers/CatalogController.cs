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

		[EnableCors("CorsPolicy")]
		[HttpGet("products/{localWarehouseID},{mainWarehouseID},{productID}")]
		public async Task<ActionResult<object>> GetProductsDetails(int localWarehouseID, int mainWarehouseID, int productID)
		{
			var products = await _context.Products.Where(p => p.ProductId == productID)
												.Select(p => new
												{
													productID = p.ProductId,
													productType = p.ProductTypeNavigation.Type,
													descriptionImage = p.DescriptionImage,
													description = p.Description,
													price = p.Price
												})
												.FirstAsync();
			var localWarehouseAviability = await _context.ProductsWarehouses.Where(p => p.Products == productID && p.Warehouses == localWarehouseID)
												.Select(p => new
												{
													localWarehouseAviability = p.Quantity
												})
												.FirstAsync();
			var mainWarehouseAviability = await _context.ProductsWarehouses.Where(p => p.Products == productID && p.Warehouses == mainWarehouseID)
												.Select(p => new
												{
													mainWarehouseAviability = p.Quantity
												})
												.FirstAsync();
			var productCard = new {
				products.productID,
				products.productType,
				products.descriptionImage,
				products.description,
				products.price,
				localWarehouseAviability.localWarehouseAviability,
				mainWarehouseAviability.mainWarehouseAviability
			};

			if (productCard == null)
			{
				return NotFound();
			}
			return productCard;
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
