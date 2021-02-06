using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace jNet.CRUD
{
	public interface IHaveId<K>
	{
		K Id { get; }
	}

	public interface ICrud<T, K>
		where T : class, IHaveId<K>, new()
	{
		Task<IEnumerable<T>> Get();
		Task<T?> Get(K id);
		Task<bool> Save(T entity);
		Task<bool> Delete(T entity) => Delete(entity.Id);
		Task<bool> Delete(K id);
		bool IsDefaultKey(K value);
	}

	public abstract class NotifyBase: INotifyPropertyChanged
	{
		private Dictionary<string, PropertyChangedEventArgs> cache = new();

		private event PropertyChangedEventHandler propertyChanged;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				propertyChanged += value;
			}

			remove
			{
				propertyChanged -= value;
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			if(!cache.TryGetValue(name, out var args))
			{
				args = new PropertyChangedEventArgs(name);
				cache[name] = args;
			}
			propertyChanged?.Invoke(this, args);
		}
	}
}

