using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoreController : ControllerBase
	{
		[HttpPost("{type}")]
		public void Post(string type, [FromBody] object data)
		{
			var path = $"Data";
			System.IO.Directory.CreateDirectory(path);
			System.IO.File.WriteAllText($"{path}/{type}.json", data.ToString());
		}

		[HttpGet("{type}")]
		public async Task<IActionResult> Get(string type)
		{
			if (System.IO.File.Exists($"Data/{type}.json"))
			{
				var x = await System.IO.File.ReadAllTextAsync($"Data/{type}.json");
				return Content(x, "application/json");
			}
			return Content("[]", "application/json");
		}
	}
}
