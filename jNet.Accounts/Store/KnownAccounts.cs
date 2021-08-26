using jNet.Accounts.Shared.Model;
using System;
using System.Net.Http;

namespace jNet.Accounts.Store
{
	public class KnownAccounts : Known<Account, Guid>
	{
		public KnownAccounts(HttpClient httpClient) : base(httpClient) { }
	}
}

