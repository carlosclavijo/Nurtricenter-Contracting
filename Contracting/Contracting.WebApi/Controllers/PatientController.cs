using Contracting.Application.Patients.CreatePatient;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Application.Patients.GetPatients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracting.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreatePatient([FromBody] CreatePatientCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            var createdPatient = await _mediator.Send(new GetPatientByIdQuery(id));

            var response = new
            {
                Patient = createdPatient,
                Message = "Patient created successfully"
            };
            return Created("", response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetPatients()
    {
        try
        {
            var result = await _mediator.Send(new GetPatientsQuery(""));
            var response = new
            {
                Total = result.Count(),
                Patients = result
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPatientById([FromRoute] Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetPatientByIdQuery(id));
            if (result == null)
            {
                var res = new
                {
                    message = "Patient not found"
                };
                return NotFound(res);
            }
            var response = new
            {
                Patient = result,
                Message = "Patient details retrieved successfully"
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
