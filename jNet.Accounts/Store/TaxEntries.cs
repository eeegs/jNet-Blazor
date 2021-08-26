using jNet.Accounts.Code;
using jNet.Accounts.Shared.Model;
using System.Net.Http;

namespace jNet.Accounts.Store
{
	public class TaxEntries : Store<TaxEntry, string>
	{
		public TaxEntries(HttpClient httpClient) : base(httpClient)
		{
		}
	}
}
