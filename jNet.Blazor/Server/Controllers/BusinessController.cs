using s = jNet.Blazor.Shared;
using m = jNet.Data.Model;
using jNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jNet.Blazor.Server.Controllers
{
	public class BusinessController : BaseController
	{
		public BusinessController(AccountingDb accountingDb, ILogger<BusinessController> logger) : base(accountingDb, logger)
		{
		}

		// GET: api/<BusinessController>
		[HttpGet]
		public async Task<IEnumerable<s.Business>> Get()
		{
			var x = await dB.Businesses.Include(q => q.Detail).Select(q=>Map(q)).ToListAsync();
			logger.LogInformation($"{x.Count} business entries returned.");
			return x;
		}

		// GET api/<BusinessController>/5
		[HttpGet("{id}")]
		public async Task<s.Business> Get(long id)
		{
			try
			{
				var x = await dB.Businesses.Include(q => q.Detail).SingleAsync(q => q.Id == id);
				return Map(x);
			}
			catch (InvalidOperationException x)
			{
				logger.LogWarning(x, $"{id} is not a valid Business Id.");
				throw new IndexOutOfRangeException();
			}
			catch (Exception x)
			{
				logger.LogWarning(x, x.Message);
				throw;
			}
		}

		// POST api/<BusinessController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<BusinessController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<BusinessController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	
		static s.Business Map(m.Business b)
		{
			var res = new s.Business
			{
				ABN = b.Detail.ABN,
				ACN = b.Detail.ACN,
				AccountId = b.AccountId,
				Id = b.Id,
				Name = b.Name,
				Description = b.Detail.Description
			};
			return res;
		}
	}
}
