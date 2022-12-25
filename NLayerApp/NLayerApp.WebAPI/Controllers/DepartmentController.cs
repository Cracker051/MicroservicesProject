using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.BLL.Exceptions;
using NLayerApp.BLL.Services;
using NLayerApp.BLL.DTO;
using NLayerApp.DAL.Entities;
using NLayerApp.WebAPI.RabbitMQ;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Net;

namespace NLayerApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentService;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public DepartmentController(IDepartmentServices departmentService, IRabbitMQProducer rabbitMQProducer)
        {
            _departmentService = departmentService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentsDTO>))]
        public IActionResult GetDepartments()
        {
            return Ok(_departmentService.GetDepartments());
        }

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentById(int departmentId)
        {
            try {
                return Ok(_departmentService.GetById(departmentId));
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpGet("getByName/{name}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentByName(string name)
        {
            try
            {
                return Ok(_departmentService.GetByName(name));
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentsDTO>))]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] DepartmentsDTO departmentCreate)
        {
            try
            {
                var department = _departmentService.Create(departmentCreate);
                _rabbitMQProducer.SendDepartmentMessage(department);
                return Ok(department);
            }
            catch (ArleadyExistsException ex)
            {
                ModelState.AddModelError("", "Department already exists!");
                return StatusCode(422, ModelState);
            }
        }

        [HttpPut("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentsDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int departmentId,[FromBody] DepartmentsDTO department)
        {
            try
            {
                return Ok(_departmentService.Update(departmentId, department));
            }
            catch(ArgumentException ex)
            {
                    ModelState.AddModelError("", ex.Message);
                    return BadRequest(ModelState);
            }
            catch(NotFoundException ex)
            {
                ModelState.AddModelError("", "Department with this id doesnt exists!");
                return BadRequest(ModelState);
            }
            catch(ModelErrorException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            try
            {
                _departmentService.Delete(departmentId);
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
