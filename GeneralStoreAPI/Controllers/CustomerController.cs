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
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        //POST api/customer
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Customer customerToCreate)
        {
            Customer createdCustomer = _context.Customers.Add(customerToCreate);

            await _context.SaveChangesAsync();

            return Ok(createdCustomer);
        }

        //GET api/customer
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

            return Ok(customers);
        }

        //Get api/customer/:id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetCustomerByID(int id)
        {
            Customer requestCustomer = await _context.Customers.FindAsync(id);

            if (requestCustomer == null)
            {
                return NotFound();
            }

            return Ok(requestCustomer);
        }

        //Put api/customer/:id
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            Customer requestedCustomer = await _context.Customers.FindAsync(id);

            if (requestedCustomer == null)
            {
                return NotFound();
            }

            if (updatedCustomer.Name != null)
            {
                requestedCustomer.Name = updatedCustomer.Name;
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(requestedCustomer);
            }

            catch (Exception e)
            {
                return (BadRequest(e.Message));
            }
        }

        //Delete api/customer/:id
        [HttpDelete]
        [Route("{id})")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            Customer requestedCustomer = await _context.Customers.FindAsync(id);

            if(requestedCustomer == null)
            {
                return NotFound();
            }

            try
            {
                _context.Customers.Remove(requestedCustomer);
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
