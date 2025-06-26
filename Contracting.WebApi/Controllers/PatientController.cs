using Contracting.Application.Patients.CreatePatient;
using Contracting.Application.Patients.GetPatientById;
using Contracting.Application.Patients.GetPatients;
using Contracting.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracting.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController(IMediator Mediator) : CustomController
{
    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand command)
    {
        try
        {
            var id = await Mediator.Send(command);
			var createdPatient = await Mediator.Send(new GetPatientByIdQuery(id.Value));
			var response = new
			{
				Patient = createdPatient.Value,
				Message = "Patient created successfully"
			};
			return Ok(response);
		}
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        try
        {
            var result = await Mediator.Send(new GetPatientsQuery(""));
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
            var result = await Mediator.Send(new GetPatientByIdQuery(id));
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
                Patient = result.Value,
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
