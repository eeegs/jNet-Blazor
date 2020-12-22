using jNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Blazor.Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/[controller]")]
	public class BaseController : ControllerBase
	{
		protected AccountingDb dB { get; }
		protected ILogger<ControllerBase> logger { get; }

		public BaseController(AccountingDb accountingDb, ILogger<ControllerBase> logger)
		{
			dB = accountingDb;
			this.logger = logger;
		}
	}
}
