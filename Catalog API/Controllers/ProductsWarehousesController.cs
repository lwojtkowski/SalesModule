using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog_API.Models;

namespace Catalog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsWarehousesController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public ProductsWarehousesController(CatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductsWarehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsWarehouses>>> GetProductsWarehouses()
        {
            return await _context.ProductsWarehouses.ToListAsync();
        }

        // GET: api/ProductsWarehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsWarehouses>> GetProductsWarehouses(int id)
        {
            var productsWarehouses = await _context.ProductsWarehouses.FindAsync(id);

            if (productsWarehouses == null)
            {
                return NotFound();
            }

            return productsWarehouses;
        }

        // PUT: api/ProductsWarehouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductsWarehouses(int id, ProductsWarehouses productsWarehouses)
        {
            if (id != productsWarehouses.Warehouses)
            {
                return BadRequest();
            }

            _context.Entry(productsWarehouses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsWarehousesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsWarehouses
        [HttpPost]
        public async Task<ActionResult<ProductsWarehouses>> PostProductsWarehouses(ProductsWarehouses productsWarehouses)
        {
            _context.ProductsWarehouses.Add(productsWarehouses);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductsWarehousesExists(productsWarehouses.Warehouses))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductsWarehouses", new { id = productsWarehouses.Warehouses }, productsWarehouses);
        }

        // DELETE: api/ProductsWarehouses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductsWarehouses>> DeleteProductsWarehouses(int id)
        {
            var productsWarehouses = await _context.ProductsWarehouses.FindAsync(id);
            if (productsWarehouses == null)
            {
                return NotFound();
            }

            _context.ProductsWarehouses.Remove(productsWarehouses);
            await _context.SaveChangesAsync();

            return productsWarehouses;
        }

        private bool ProductsWarehousesExists(int id)
        {
            return _context.ProductsWarehouses.Any(e => e.Warehouses == id);
        }
    }
}
