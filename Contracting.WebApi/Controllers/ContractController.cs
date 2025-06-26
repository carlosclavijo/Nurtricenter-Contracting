using Contracting.Application.Contracts.CompleteContract;
using Contracting.Application.Contracts.CreateContract;
using Contracting.Application.Contracts.DeleteDeliveryDay;
using Contracting.Application.Contracts.GetContractById;
using Contracting.Application.Contracts.GetContracts;
using Contracting.Application.Contracts.GetDeliveryDay;
using Contracting.Application.Contracts.InProgressContract;
using Contracting.Application.Contracts.UpdateDeliveryDayById;
using Contracting.Application.Contracts.UpdateDeliveryDays;
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
				Contract = createdContract.Value,
				Message = "Contract created successfully"
			};
            return Ok(response);
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
					message = "Contract not found"
				};
				return NotFound(res);
			}
			var response = new
			{
				Contract = result.Value,
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
			if (result)
			{
				var contract = await Mediator.Send(new GetContractByIdQuery(command.ContractId));
				var response = new
				{
					Message = "Contract is now in progress",
					Contract = contract.Value
				};
				return Ok(response);
			}
			else
			{
				var response = new
				{
					Message = "Failed to set contract in progress"
				};
				return BadRequest(response);
			}
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
			if (result)
			{
				var contract = await Mediator.Send(new GetContractByIdQuery(command.ContractId));
				var response = new
				{
					Message = "Contract is now Completed",
					Contract = contract.Value
				};
				return Ok(response);
			}
			else
			{
				var response = new
				{
					Message = "Failed to set contract complete"
				};
				return BadRequest(response);
			}
		}
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

	[HttpGet]
	[Route("Delivery/{id}")]
	public async Task<IActionResult> GetDeliveryDay([FromRoute] Guid id)
	{
		try
		{
			var result = await Mediator.Send(new GetDeliveryDayQuery(id));
			if (result == null)
			{
				var res = new
				{
					message = "Delivery not found"
				};
				return NotFound(res);
			}
			var response = new
			{
				Delivery = result.Value,
				Message = "Delivery details retrieved successfully"
			};
			return Ok(response);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPatch]
	[Route("DeliveryDays")]
	public async Task<IActionResult> UpdateDeliveryDays([FromBody] UpdateDeliveryDaysCommand command)
	{
		try
		{
			var id = await Mediator.Send(command);
			var createdContract = await Mediator.Send(new GetContractByIdQuery(id.Value));
			var response = new
			{
				Contract = createdContract.Value,
				Message = "Addresses changed successfully"
			};
			return Ok(response);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPatch]
	[Route("DeliveryDay")]
	public async Task<IActionResult> UpdateDeliveryDay([FromBody] UpdateDeliveyDayByIdCommand command)
	{
		try
		{
			var id = await Mediator.Send(command);
			var createdContract = await Mediator.Send(new GetContractByIdQuery(id.Value));
			var response = new
			{
				Contract = createdContract.Value,
				Message = "Address changed successfully"
			};
			return Ok(response);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpDelete]
	[Route("DeliveryDay")]
	public async Task<IActionResult> DeleteDeliveryDay([FromBody] DeleteDeliveryDayCommand command)
	{
		try
		{
			var id = await Mediator.Send(command);
			var createdContract = await Mediator.Send(new GetContractByIdQuery(id.Value));
			var response = new
			{
				Contract = createdContract.Value,
				Message = "Delivery day deleted successfully"
			};
			return Ok(response);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
}