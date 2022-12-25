using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Drugstore.Queries.GetAllDrugstores;
using Application.Drugstore.Queries.GetDrugstoreById;
using Application.Drugstore.Commands.CreateDrugstore;
using Application.Drugstore.Commands.DeleteDrugstore;
using Application.Drugstore.Commands.UpdateDrugstore;
using Application.Common.Exceptions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugstoreController : ControllerBase
    {
        private IMediator _mediator;

        public DrugstoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDrugstores()
        {
            var drugstores = await _mediator.Send(new GetAllDrugstoresQuery() { });
            return Ok(drugstores);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDrugsotreById(int id)
        {
            try
            {
                var drugstore = await _mediator.Send(new GetDepartmentByIdQuery() { Id = id });
                return Ok(drugstore);
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
        public async Task<IActionResult> AddDrugstore([FromBody] CreateDrugstoreCommand command)
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
        public async Task<IActionResult> UpdateDrugstore(int id, [FromBody] UpdateDrugstoreCommand command)
        {
            if (id != command.Id)
            {
                ModelState.AddModelError("", "Id's must be equal");
                return BadRequest(ModelState);
            }
            try
            {
                var drugstore = await _mediator.Send(command);
                return Ok(drugstore);
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
        public async Task<IActionResult> DeleteDrugstore([FromQuery] DeleteDrugstoreCommand command)
        {
            try
            {
                var drugstore = await _mediator.Send(command);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound(ModelState);
            }
        }
    }
}

