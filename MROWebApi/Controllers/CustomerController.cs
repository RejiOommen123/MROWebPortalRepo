using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirstMigration.Context;
using MROWebAPI.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customer.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("GetCustomers/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Customer>> GetCustomers(string id)
        {
            var customer = await _context.Customer.FindAsync(int.Parse(id));

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("EditCustomer/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            int id = customer.CustomerId;
            List<FieldCustomerMap> fieldCustomerMaps= new List<FieldCustomerMap>();
            foreach (Field field in _context.Field)
            {
                fieldCustomerMaps.Add(new FieldCustomerMap
                {
                    FieldId = field.FieldId,
                    CustomerId = id,
                    IsEnable = true
                });
            }
            _context.UpdateRange(fieldCustomerMaps);

            List<WizardCustomerMap> wizardCustomerMaps = new List<WizardCustomerMap>();
            foreach (Wizard wizard in _context.Wizard)
            {
                wizardCustomerMaps.Add(new WizardCustomerMap
                {
                    WizardId = wizard.WizardId,
                    CustomerId = id,
                    IsEnable = true
                });
            }
            _context.UpdateRange(wizardCustomerMaps);
            _context.SaveChanges();
            return Ok("success");
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
        [HttpGet("EditFields/{id}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFields(int id)
        {
           var wizards= (from fcm in _context.FieldCustomerMap
                         join f in _context.Field
                         on fcm.FieldId.ToString() equals f.FieldId.ToString()
                         where fcm.CustomerId == id
                         select new
                         {
                             FieldCustomerMapId = fcm.FieldCustomerMapId,
                             FieldId = fcm.FieldId,
                             CustomerId = fcm.CustomerId,
                             IsEnable = fcm.IsEnable,
                             FieldName = f.FieldName,
                             WizardId = f.WizardId                            
                         }).ToList();
            Customer cust = _context.Customer.Where(c => c.CustomerId == id).FirstOrDefault();
            var custName = cust.CustomerName;
            return Ok(new { wizards, custName });
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> EditFields([FromBody]FieldCustomerMap[] fieldCustomerMaps)
        {
            _context.UpdateRange(fieldCustomerMaps);
            _context.SaveChanges();
            return Ok();
        }
    }
}
