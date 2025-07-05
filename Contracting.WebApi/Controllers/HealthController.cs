using Microsoft.AspNetCore.Mvc;

namespace NutritionalAdvice.WebApi.Controllers
{
	[ApiController]
	[Route("health")]
	public class HealthController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetHealth()
		{
			return Ok(new { status = "ok" });
		}
	}
}