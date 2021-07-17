using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace jNet.Temporal
{

	public interface IGCollection<T> : ICollection<T>
	{
		void Add(params T[] items);
		void Add(IEnumerable<T> items);
	}

	public class Graph
	{
		readonly GSet<Node> _nodes = new();
		readonly GSet<Edge> _edges = new();

		public IGCollection<Node> Nodes => _nodes;
		public IGCollection<Edge> Edges => _edges;

		public void Save()
		{
			_nodes.Save();
			_edges.Save();
		}

		private class GSet<T> : IGCollection<T>
			where T : Base
		{
			readonly HashSet<T> New = new();
			readonly Dictionary<Guid, T> Existing = new();
			readonly HashSet<T> Removed = new();

			public void Save()
			{
				foreach (var i in Existing.Values.Where(q => q.state == SaveState.Modified))
				{
					Persist(i);
					i.state = SaveState.Original;
				}

				foreach (var i in New)
				{
					i.Id = Guid.NewGuid();
					Persist(i);
					i.state = SaveState.Original;
					Existing[i.Id] = i;
				}
				New.Clear();
			}

			private static void Persist(T item)
			{
				var path = $@"c:\users\sscot\data\{item.Type}\{item.Category}";
				System.IO.Directory.CreateDirectory(path);
				using var a = System.IO.File.CreateText($@"{path}\{item.Id}.ini");

				a.WriteLine("[Control]");
				a.WriteLine($"Type={item.Type}");
				a.WriteLine($"Category={item.Category}");
				a.WriteLine($"Id={item.Id}");
				a.WriteLine($"Start={item.Start}");
				a.WriteLine($"End={item.End}");
				a.WriteLine("[Fields]");
				foreach (var f in item)
				{
					a.WriteLine($"{f}={item[f]}");
					var fpath = $@"c:\users\sscot\data\{item.Type}\{item.Category}\Field\{f}";
					System.IO.Directory.CreateDirectory(fpath);
					using var fa = System.IO.File.OpenWrite($@"{fpath}\{item[f].Substring(0, Math.Min(item[f].Length, 8)),-8}.bin");








					fa.Seek(0, System.IO.SeekOrigin.End);
					fa.Write(item.Id.ToByteArray(), 0, 16);
					fa.Close();
				}
				a.Close();
			}


			public int Count => New.Count + Existing.Count;

			public bool IsReadOnly => false;

			public void Add(T item)
			{
				//Contract.Requires<InvalidOperationException>(item.Id == Guid.Empty, "You can only add new items to the set.");
				New.Add(item);
			}

			public void Clear()
			{
				New.Clear();
				Existing.Clear();
			}

			public bool Contains(T item)
			{
				return New.Contains(item) || Existing.ContainsValue(item);
			}

			public void CopyTo(T[] array, int arrayIndex)
			{
				throw new NotImplementedException();
			}

			public IEnumerator<T> GetEnumerator()
			{
				foreach (T i in New) yield return i;
				foreach (T i in Existing.Values) yield return i;
			}

			public bool Remove(T item)
			{
				if (item.Id == Guid.Empty)
				{
					return New.Remove(item);
				}
				else
				{
					if (Existing.Remove(item.Id))
					{
						Removed.Add(item);
						return true;
					}
					return false;
				}
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

			public void Add(params T[] items) => _Add(items);
			public void Add(IEnumerable<T> items) => _Add(items);

			void _Add(IEnumerable<T> items)
			{
				foreach (T i in items)
				{
					Add(i);
				}
			}
		}
	}
}
