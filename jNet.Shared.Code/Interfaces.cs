using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Shared.Code
{
	public interface IHaveKey
	{
		Guid Key { get; init; }
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
