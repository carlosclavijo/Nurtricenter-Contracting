using Contracting.Application.Administrators.CreateAdministrator;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using Contracting.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracting.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdministratorController(IMediator Mediator) : CustomController
{
    [HttpPost]
    public async Task<IActionResult> CreateAdministrator([FromBody] CreateAdministratorCommand command)
    {
        try
        {
            var id = await Mediator.Send(command);
            var createdAdministrator = await Mediator.Send(new GetAdministratorByIdQuery(id.Value));
            var response = new
            {
                Administrator = createdAdministrator,
                Message = "Administrator created successfully"
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAdministrators()
    {
        try
        {
            var result = await Mediator.Send(new GetAdministratorsQuery(""));
            var response = new
            {
                Total = result.Count(),
                Administrators = result
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
    public async Task<IActionResult> GetAdministratorById([FromRoute] Guid id)
    {
        try
        {
            var result = await Mediator.Send(new GetAdministratorByIdQuery(id));
            if (result == null)
            {
                var res = new
                {
                    message = "Administrator not found"
                };
                return NotFound(res);
            }
            var response = new
            {
                Administrator = result,
                Message = "Administrator details retrieved successfully"
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
