using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog_API.Models;
using Microsoft.EntityFrameworkCore;

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
		[HttpGet("products/{id}")]
		public async Task<List<object>> GetProducts(int id)
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
												.ToListAsync<object>();
			return products;
		}

		//[HttpGet("/{warehouseID},{productID}")]
		//public async Task<List<object>> GetProducts(int warehouseID, int productID)
		//{
		//	var products = await _context.ProductsWarehouses.Where(p => p.Products == productID)
		//										.Select(p => new
		//										{
		//											productID = p.ProductId,
		//											descriptionImage = p.DescriptionImage,
		//											description = p.Description,
		//											price = p.Price
		//										})
		//										.ToListAsync<object>();
		//	return products;
		//}
	}
}
