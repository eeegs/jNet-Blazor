using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.CRUD
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Linq.Expressions;
	using System.Collections.Concurrent;
	using System.Threading.Tasks;

	public interface ICachable
	{
		bool IsLoaded { get; }
		void Clear();
	}

	public interface ICritera<T>
		where T : class
	{
		T Value { get; }
		DateTime LastModified { get; }
	}

	public interface IRepository<T, Tc, Tk> : IDisposable
		where T : class
		where Tc : ICritera<T>
		where Tk : struct
	{
		T[] Get();
		T[] Get(Expression<Func<Tc, bool>> specification);
		Tk[] GetKeys(Expression<Func<Tc, bool>> specification);
		T[] Get(Tk[] keys);
		T this[Tk index] { get; }

		//void Delete(TE entity);
		//void Add(TE entity);
		//int SaveChanges();
		//DbTransaction BeginTransaction();
	}

	public abstract class Critera<T> : ICritera<T>
		where T : class
	{
		public T Value { get; set; }
		public DateTime LastModified { get; set; }
		public static explicit operator T(Critera<T> arg) { return arg.Value; }
	}

	public abstract class Repository<T, Tc, Tk> : IRepository<T, Tc, Tk>
		where T : class
		where Tc : ICritera<T>
		where Tk : struct
	{
		public virtual T[] Get(Expression<Func<Tc, bool>> specification)
		{
			var keys = GetKeys(specification);
			return Get(keys);
		}

		public abstract Tk[] GetKeys(Expression<Func<Tc, bool>> specification);
		public abstract T[] Get(Tk[] keys);

		public virtual T[] Get()
		{
			return Get(x => true);
		}

		public virtual T this[Tk index]
		{
			get { return Get(new[] { index }).Single(); }
		}

		public virtual void Dispose()
		{
		}
	}

	public abstract class CachedRepository<T, Tc, Tk> : Repository<T, Tc, Tk>
		where T : class
		where Tc : ICritera<T>
		where Tk : struct
	{
		private readonly ConcurrentDictionary<Tk, Tuple<long, T>> _cache = new ConcurrentDictionary<Tk, Tuple<long, T>>();
		private readonly IRepository<T, Tc, Tk> _repository;
		private readonly long _lifeTime;

		public CachedRepository(IRepository<T, Tc, Tk> repository, TimeSpan lifeTime)
		{
			_repository = repository;
			_lifeTime = lifeTime.Ticks;
		}

		protected abstract Func<T, Tk> Key { get; }

		public override Tk[] GetKeys(Expression<Func<Tc, bool>> specification)
		{
			return _repository.GetKeys(specification);
		}

		public override T[] Get(Tk[] keys)
		{
			Tuple<long, T> item = default;
			var now = DateTime.Now.Ticks;
			var age = now - _lifeTime;

			var cached = from i in keys
						 where
							_cache.TryGetValue(i, out item) &&
							item.Item1 > age
						 select i;

			var needed = keys.Except(cached);

			var data = _repository.Get(needed.ToArray());

			foreach (var d in data)
			{
				_cache[Key(d)] = Tuple.Create(now, d);
			}

			var s = from k in keys
					where _cache.TryGetValue(k, out item)
					select item.Item2;
			return s.ToArray();
		}
	}

	public abstract class CachedRepository2<T, Tc, Tk> : Repository<T, Tc, Tk>
		where T : class
		where Tc : ICritera<T>
		where Tk : struct
	{
		protected readonly object LockMe = new object();
		protected readonly IRepository<T, Tc, Tk> _repository;
		protected Dictionary<Tk, T> _cache = null;
		protected TimeSpan _lifeTime;

		public CachedRepository2(IRepository<T, Tc, Tk> repository, TimeSpan lifeTime)
		{
			_repository = repository;
			_lifeTime = lifeTime;
		}

		protected abstract Func<T, Tk> Key { get; }

		public override Tk[] GetKeys(Expression<Func<Tc, bool>> specification)
		{
			return _repository.GetKeys(specification);
		}

		public override T[] Get(Tk[] keys)
		{
			lock (LockMe)
			{
				if (_cache == null)
				{
					Load();
					new System.Threading.Timer(Clear, null, (long)_lifeTime.TotalMilliseconds, -1);
				}

				T item = null;

				var s = from k in keys
						where _cache.TryGetValue(k, out item)
						select item;
				return s.ToArray();
			}
		}

		protected void Load()
		{
			lock (LockMe)
			{
				_cache = _repository.Get().ToDictionary(Key);
			}
		}

		protected virtual void Clear(object state = null)
		{
			lock (LockMe)
			{
				if (_cache != null)
				{
					_cache.Clear();
					_cache = null;
				}
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			Clear();
		}
	}

	public abstract class CachedRepository3<T, Tc, Tk> : CachedRepository2<T, Tc, Tk>
		where T : class
		where Tc : ICritera<T>
		where Tk : struct
	{
		public CachedRepository3(IRepository<T, Tc, Tk> repository, TimeSpan lifeTime)
			: base(repository, lifeTime)
		{
		}

		protected override void Clear(object state = null)
		{
			Task.Factory.StartNew<Dictionary<Tk, T>>(() =>
			{
				return _repository.Get().ToDictionary(Key);
			}).ContinueWith(t =>
			{
				lock (LockMe)
				{
					base.Clear();
					_cache = t.Result;
				}
				new System.Threading.Timer(Clear, null, (long)_lifeTime.TotalMilliseconds, -1);
			});
		}
	}

	/*
	public abstract class BaseRepository<T, Tc, Tk, Td, TdB> : Repository<T, Tc, Tk>
		where T : class
		where Tc : ICritera<T>
		where Tk : struct
		where Td : class
		where TdB : IDisposable, new()
	{
		protected abstract Expression<Func<Td, Tc>> Map { get; }
		protected abstract Expression<Func<Tc, Tk>> Key { get; }
		//protected abstract Expression<Func<Td, Tc>> _map { get; }

		//public override T[] Get(System.Linq.Expressions.Expression<Func<Tc, bool>> specification)
		//{
		//    using (var db = new TdB())
		//    {
		//        return db.GetTable<Td>().Select(Map).Where(specification).Select(q => q.Value).ToArray();
		//    }
		//}

		public override Tk[] GetKeys(System.Linq.Expressions.Expression<Func<Tc, bool>> specification)
		{
			using (var db = new TdB())
			{
				return db.GetTable<Td>().Select(Map).Where(specification).Select(Key).ToArray();
			}
		}

		public override T[] Get(Tk[] keys)
		{
			using (var db = new TdB())
			{
				db.Log = Console.Out;
				var num = (keys.Length - 1) / 500 + 1;
				var result = from i in Enumerable.Range(0, num).AsParallel()
							 let chunk = keys.Skip(i).Take(500)
							 from x in db.GetTable<Td>().Where(SelectKeys(chunk)).Select(Map)
							 select x.Value;
				return result.ToArray();
			}
		}

		public abstract Expression<Func<Td, bool>> SelectKeys(IEnumerable<Tk> keys);
	}
	*/
}