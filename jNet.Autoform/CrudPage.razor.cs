using jNet.CRUD;
using Microsoft.AspNetCore.Components;

namespace jNet.Autoform
{
	public partial class CrudPage<TModel, TKey>
		where TModel : class, IHaveId<TKey>, new()
	{
		[Inject]
		protected NavigationManager? NavigationManager { get; private set; }
	}
}
