using s = jNet.Blazor.Shared;
using m = jNet.Data.Model;
using jNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jNet.Blazor.Server.Controllers
{
	public class AccountController : BaseController
	{
		public AccountController(AccountingDb accountingDb, ILogger<ControllerBase> logger) : base(accountingDb, logger)
		{
		}

		// GET: api/<AccountController>
		[HttpGet]
		public async Task<IEnumerable<s.Account>> Get()
		{
			var x = await dB.Accounts.Select(q => MapTransport(q)).ToListAsync();
			logger.LogInformation($"{x.Count} accounts entries returned.");
			return x;
		}

		// GET api/<AccountController>/5
		[HttpGet("{id}")]
		public async Task<s.Account> Get(long id)
		{
			try
			{
				var x = await dB.Accounts.SingleAsync(q => q.Id == id);
				return MapTransport(x);
			}
			catch (InvalidOperationException x)
			{
				logger.LogWarning(x, $"{id} is not a valid Account Id.");
				throw new IndexOutOfRangeException();
			}
			catch (Exception x)
			{
				logger.LogWarning(x, x.Message);
				throw;
			}
		}


		// POST api/<AccountController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<AccountController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<AccountController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

		static s.Account MapTransport(m.Account data)
		{
			var res = new s.Account
			{
				Id = data.Id,
				Name = data.Name,
				Description = data.Description,
				AccountNumber = data.AccountNumber,
				IsSummaryAccount = data.IsSummaryAccount,
				Type = data.Type.ToString(),
				ParentId = data.ParentId,
				Direction = data.Direction
			};
			return res;
		}
	}
}
