using jNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Blazor.Server.Controllers
{
	[Authorize]
	[CheckBusiness]
	[ApiController]
	[Route("/api/[controller]")]
	public abstract class BaseController:ControllerBase
	{
		protected AccountingDb dB { get; }
		protected ILogger<ControllerBase> logger { get; }
		protected long BusinessId { get; private set; }
		public BaseController(AccountingDb accountingDb, ILogger<ControllerBase> logger)
		{
			dB = accountingDb;
			this.logger = logger;
		}

		private class CheckBusiness : ActionFilterAttribute
		{
			public override void OnActionExecuting(ActionExecutingContext context)
			{
				if (context.Controller is BaseController bc)
				{
					long businessId = default;
					if (context.HttpContext.Request.Query.TryGetValue("BID", out var value))
					{
						if (value.Count == 1)
						{
							if (long.TryParse(value[0], out businessId))
							{
							}
						}
					}
					bc.BusinessId = businessId;
					bc.dB.BusinessId = businessId;
					if(businessId != 0 && context.Controller is BusinessController)
					{
						throw new UnauthorizedAccessException();
					}
					if (businessId == 0 && context.Controller is not BusinessController)
					{
						throw new UnauthorizedAccessException();
					}
				}
				base.OnActionExecuting(context);
			}
		}
	}
}
