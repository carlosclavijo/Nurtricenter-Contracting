using Contracting.Application.Administrators.GetAdministratorById;
using Contracting.Application.Contracts.CompleteContract;
using Contracting.Application.Contracts.CreateContract;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Application.Contracts.InProgressContract;
using Contracting.Application.Contracts.UpdateAddressById;
using Contracting.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contracting.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractController(IMediator Mediator) : CustomController
{
	[HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractCommand command)
    {
        try
        {
            var id = await Mediator.Send(command);
            var createdContract = await Mediator.Send(new GetContractByIdQuery(id.Value));
			var response = new
			{
				Contract = createdContract,
				Message = "Contract created successfully"
			};
            return Ok(createdContract);
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
            var result = await Mediator.Send(new GetContractsQuery(""));
            var response = new
            {
                Total = result.Count(),
                Contracts = result
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
	public async Task<IActionResult> GetContractById([FromRoute] Guid id)
	{
		try
		{
			var result = await Mediator.Send(new GetContractByIdQuery(id));
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
				Contract = result,
				Message = "Contract details retrieved successfully"
			};

			return Ok(response);
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
            bool result = await Mediator.Send(command);
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
            bool result = await Mediator.Send(command);
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
            bool result = await Mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}