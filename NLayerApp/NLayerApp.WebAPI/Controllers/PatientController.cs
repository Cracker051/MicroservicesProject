using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.BLL.Exceptions;
using NLayerApp.BLL.Services;
using NLayerApp.BLL.DTO;
using NLayerApp.DAL.Entities;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NLayerApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientServices _patientServices;

        public PatientController(IPatientServices patientServices)
        {
            _patientServices = patientServices;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetPatients()
        {
            return Ok(_patientServices.GetPatients());
        }
        [HttpGet("{patientId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientById(int patientId)
        {
            try
            {
                return Ok(_patientServices.GetPatientById(patientId));
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
        }
        [HttpGet("getBySurname/{surname}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientsBySurname(string surname)
        {
            try
            {
                var entities = _patientServices.GetPatientsBySurname(surname);
                return Ok(entities);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
        }
        [HttpGet("getByDiagnosis/{diagnosis}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetPatientsByDiagnosis(string diagnosis)
        {
            try
            {
                var entities = _patientServices.GetPatientsByDiagnosis(diagnosis);
                return Ok(entities);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult CreatePatients([FromBody] PatientsDTO patient)
        {
            try
            {
                return Ok(_patientServices.CreatePatient(patient));
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
        }
        [HttpPut("{patientId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientsDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int patientId, [FromBody] PatientsDTO patient)
        {
            try
            {
                return Ok(_patientServices.UpdatePatient(patientId, patient));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
            catch (ModelErrorException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{patientId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int patientId)
        {
            try
            {
                _patientServices.DeletePatient(patientId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (ModelErrorException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
