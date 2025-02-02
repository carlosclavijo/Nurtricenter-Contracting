using Contracting.Application.Contracts.CompleteContract;
using Contracting.Application.Contracts.CreateContract;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Application.Contracts.InProgressContract;
using Contracting.Application.Contracts.UpdateAddressById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contracting.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContractController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetContractById([FromRoute] Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetContractByIdQuery(id));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetContracts()
    {
        try
        {
            var result = await _mediator.Send(new GetContractsQuery(""));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("InProgress")]
    public async Task<IActionResult> InProgressContract([FromBody] InProgressContractCommand command)
    {
        try
        {
            bool result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("Complete")]
    public async Task<IActionResult> CompleteContract([FromBody] CompleteContractCommand command)
    {
        try
        {
            bool result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("UpdateAddressDates")]
    public async Task<IActionResult> UpdateAddressDates([FromBody] UpdateAddressCommand command)
    {
        try
        {
            bool result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}