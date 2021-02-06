using jNet.CRUD;
using System;

namespace jNet.Autoform
{
	public enum CrudActivity
	{
		Create,
		List,
		Read,
		Edit,
		Delete
	}

	public interface ICrudView<TModel, TKey> : ICrud<TModel, TKey>
		where TModel : class, IHaveId<TKey>, new()
	{
		Func<TKey?, string> ID2URI { get; set; }
	}

}
