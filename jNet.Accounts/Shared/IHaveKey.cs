using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Shared
{
	public interface IHaveKey<K>
	{
		K Key { get; init; }
	}
	public interface IHaveName
	{
		string Name { get; }
	}
	public interface IHaveBag<T>//: IEnumerable<KeyValuePair<string, object?>>
	{
		T? this[string key] { get; set; }
	}
}
