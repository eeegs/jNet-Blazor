using jNet.Accounts.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettingsController : ControllerBase
	{
		static JsonSerializerOptions options = new() { WriteIndented = true };

		[HttpPost]
		public async Task Post([FromBody] Setting[] data)
		{
			foreach (var s in data)
			{
				var path = $"Data/Settings/{s.UserName}";
				var entry = JsonSerializer.Serialize(s, options);
				System.IO.Directory.CreateDirectory(path);
				await System.IO.File.WriteAllTextAsync($"{path}/{s.Name}.json", entry);
			}
		}

		[HttpGet("{userName}")]
		public async Task<IActionResult> Get(string userName)
		{
			var path = $"Data/Settings/{userName}";

			if (System.IO.Directory.Exists(path))
			{
				var x = from f in System.IO.Directory.EnumerateFiles($"{path}/*.json")
						select System.IO.File.ReadAllTextAsync(f);
				var y = await Task.WhenAll(x);
				var z = string.Join(",", y);
				return Content($"[{z}]", "application/json");
			}
			return Content("[]", "application/json");
		}
	}
}
