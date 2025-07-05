﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionalAdvice.WebApi.Controllers
{
	[ApiController]
	[Route("/")]
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Redirect("/swagger");
		}
	}
}