using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;
using Application.Department.Queries.GetAllDepartments;
using Application.Department.Queries.GetDepartmentById;
using Application.Department.Commands.CreateDepartment;
using Application.Department.Commands.DeleteDepartment;
using Application.Department.Commands.UpdateDepartment;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _mediator.Send(new GetAllDepartmentsQuery() { });
            return Ok(departments);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _mediator.Send(new GetDepartmentByIdQuery() { Id = id });
                return Ok(department);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDepartment(int id,[FromBody] UpdateDepartmentCommand command)
        {
            if (id != command.Id)
            {
                ModelState.AddModelError("", "Id's must be equal");
                return BadRequest(ModelState);
            }
            try
            {
                var department = await _mediator.Send(command);
                return Ok(department);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDepartment([FromQuery]DeleteDepartmentCommand command)
        {
            try
            {
                var department = await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
        }
    }
}
