using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        //POST api/product
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Product productToCreate)
        {
            Product createdProduct = _context.Products.Add(productToCreate);

            await _context.SaveChangesAsync();

            return Ok(createdProduct);
        }

        //GET api/product
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        //Get api/product/:id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProductByID(int id)
        {
            Product requestProduct = await _context.Products.FindAsync(id);

            if (requestProduct == null)
            {
                return NotFound();
            }

            return Ok(requestProduct);
        }

        //Put api/product/:id
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] Product updatedProduct)
        {
            Product requestedProduct = await _context.Products.FindAsync(id);

            if (requestedProduct == null)
            {
                return NotFound();
            }

            if (updatedProduct.Name != null)
            {
                requestedProduct.Name = updatedProduct.Name;
            }

            if (updatedProduct.Price < 0)
            {
                requestedProduct.Price = updatedProduct.Price;
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(requestedProduct);
            }

            catch (Exception e)
            {
                return (BadRequest(e.Message));
            }
        }

        //Delete api/product/:id
        [HttpDelete]
        [Route("{id})")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            Product requestedProduct = await _context.Products.FindAsync(id);

            if (requestedProduct == null)
            {
                return NotFound();
            }

            try
            {
                _context.Products.Remove(requestedProduct);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
