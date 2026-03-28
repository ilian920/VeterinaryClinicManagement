using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services;

namespace VeterinaryClinicMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly List<Patient> _patients = new List<Patient>();

        // GET: api/patients
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(_patients);
        }

        // GET: api/patients/{id}
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        public ActionResult<Patient> CreatePatient([FromBody] Patient patient)
        {
            if (patient == null) return BadRequest();
            _patients.Add(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        // PUT: api/patients/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] Patient patient)
        {
            var existingPatient = _patients.FirstOrDefault(p => p.Id == id);
            if (existingPatient == null) return NotFound();
            existingPatient.Name = patient.Name;
            // Update other properties
            return NoContent();
        }

        // DELETE: api/patients/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            if (patient == null) return NotFound();
            _patients.Remove(patient);
            return NoContent();
        }
    }
}