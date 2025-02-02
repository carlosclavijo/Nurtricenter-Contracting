using Contracting.Application.Administrators.CreateAdministrator;
using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Administrators.GetAdministrators;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contracting.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdministratorController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdministratorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAdministrator([FromBody] CreateAdministratorCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetAdministrators()
    {
        try
        {
            var result = await _mediator.Send(new GetAdministratorsQuery(""));
            return Ok(result);
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
            var result = await _mediator.Send(new GetAdministratorByIdQuery(id));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
