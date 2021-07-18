using jNet.Blazor.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jNet.Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EntityController : ControllerBase
	{
		// GET: api/<Entity>
		[HttpGet]
		public DataSet Get()
		{
			var ds = DataSet.Load("fred.json");
			return ds;
		}

		// POST api/<Entity>
		[HttpPost]
		public DataSet Post([FromBody] DataSet ds)
		{
			//var ds = JsonSerializer.Deserialize<DataSet>(value);
			ds.Save("fred.json");
			return ds;
		}
	}
}
