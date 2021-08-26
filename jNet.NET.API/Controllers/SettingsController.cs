using jNet.Shared.Code;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace jNet.NET.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettingsController : ControllerBase
	{
		static JsonSerializerOptions options = new() { WriteIndented = true };

		[HttpPost]
		public async Task Post([FromBody] Setting[] data)
		{
			foreach(var s in data)
			{
				var path = $"Data/Settings/{s.Name}/";
				var entry = JsonSerializer.Serialize(s, options);
				System.IO.Directory.CreateDirectory(path);
				await System.IO.File.WriteAllTextAsync($"{path}{s.UserName}.json", entry);
			}
		}

		[HttpGet("{type}/{userName}")]
		public async Task<IActionResult> Get(string type, string userName)
		{
			var path = $"Data/Settings/{type}/{userName}.json";
			if (System.IO.File.Exists(path))
			{
				var x = await System.IO.File.ReadAllTextAsync(path);
				return Content(x, "application/json");
			}
			return Content("{}", "application/json");
		}
	}
}
