using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace VeterinaryClinicMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private static List<Invoice> invoices = new List<Invoice>(); // Placeholder for invoice data

        // GET: api/invoices
        [HttpGet]
        public ActionResult<IEnumerable<Invoice>> GetInvoices()
        {
            return Ok(invoices);
        }

        // GET: api/invoices/{id}
        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoice(int id)
        {
            var invoice = invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
                return NotFound();
            return Ok(invoice);
        }

        // POST: api/invoices
        [HttpPost]
        public ActionResult<Invoice> CreateInvoice([FromBody] Invoice newInvoice)
        {
            newInvoice.Id = invoices.Count > 0 ? invoices.Max(i => i.Id) + 1 : 1;
            invoices.Add(newInvoice);
            return CreatedAtAction(nameof(GetInvoice), new { id = newInvoice.Id }, newInvoice);
        }

        // PUT: api/invoices/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateInvoice(int id, [FromBody] Invoice updatedInvoice)
        {
            var invoice = invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
                return NotFound();
            invoice.Amount = updatedInvoice.Amount;
            invoice.Status = updatedInvoice.Status;
            return NoContent();
        }

        // DELETE: api/invoices/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            var invoice = invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
                return NotFound();
            invoices.Remove(invoice);
            return NoContent();
        }

        // POST: api/invoices/{id}/pay
        [HttpPost("{id}/pay")]
        public IActionResult PayInvoice(int id, [FromBody] PaymentInfo paymentInfo)
        {
            var invoice = invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
                return NotFound();

            // Process payment logic here (placeholder)
            invoice.Status = "Paid";
            return NoContent();
        }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }

    public class PaymentInfo
    {
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
    }
}