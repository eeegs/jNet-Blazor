using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace jNet.Temporal
{
	public struct Data : IComparable<Data>
	{
		public int Key;
		public Block? Link;

		public int CompareTo(Data other) => Key - other.Key;

		public static Data fromBlock(Block block)
		{
			return new Data { Key = block[0].Key, Link = block };
		}

		public override string ToString()
		{
			return $"{Key}: {Link?.IsLeaf}({Link?.Used}) - {Link?.GetHashCode()}";
		}
	}

	public struct InlineableFeatureValueComparer : IComparer<Data>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int Compare(Data a, Data b) => a.Key - b.Key;
	}

	public class BTree
	{
		private Block root = new(true);

		public void Add(Data value)
		{
			var newBlock = Add(root, value);
			if (newBlock != null)
			{
				var newRoot = new Block(false);
				newRoot.Add(Data.fromBlock(root));
				newRoot.Add(Data.fromBlock(newBlock));
				root = newRoot;
			}
		}

		public bool Exists(int key)
		{
			var value = new Data { Key = key };
			var cur = root;

			do
			{
				var i = cur.IndexOf(value);
				if (i >= 0) return true;
				if (cur.IsLeaf) return false;
				i = -i - 2;
				if (i < 0) return false;
				cur = cur[i].Link;
			} while (cur is not null);
			return false;
		}

		public void DrawTree()
		{
			Block? cur, left;
			left = root;
			Console.WriteLine("-------------------");
			do
			{
				cur = left;
				do
				{
					Console.Write(cur);
					cur = cur[Block.SECTOR_SIZE - 1].Link;
				} while (cur is not null);
				Console.WriteLine();
				left = left[0].Link;
			} while (left is not null);
		}

		public IEnumerable<Data> GetFrom(int start = 0, int end = int.MaxValue)
		{
			var value = new Data { Key = start };
			var cur = root;

			while(!cur!.IsLeaf)
			{
				var i = cur.IndexOf(value);
				if(i < 0)
				{
					i = -i - 2;
					if (i < 0) i++;
				}
				cur = cur[i].Link;
			};

			do
			{
				for (int i = 0; i < cur.Used; i++)
				{
					if (cur[i].Key >= start && cur[i].Key < end)
					{
						yield return cur[i];
					}
				}
				cur = cur[Block.SECTOR_SIZE - 1].Link;
			} while (cur is not null);
		}

		private Block? Add(Block current, Data value)
		{
			if (current.IsLeaf)
			{
				return current.Add(value);
			}

			var childIndex = current.GetChild(value);
			var child = current[childIndex];
			if (value.Key < child.Key)
			{
				current[childIndex] = new Data { Key = value.Key, Link = child.Link };
			}
			var newBlock = Add(child.Link!, value);
			if (newBlock != null)
			{
				// a new block needs to be added to the current
				return current.Add(Data.fromBlock(newBlock));
			}
			return null;
		}
	}

	public class Block
	{
		public const int SECTOR_SIZE = 8;

		private Data[] data = new Data[SECTOR_SIZE];

		private static char nextLabel = 'A';
		private readonly static InlineableFeatureValueComparer comp = new();
		private readonly char label;
		internal int Used = 0;
		public bool IsLeaf { get; }

		public Data this[int index] { get => data[index]; set => data[index] = value; }

		public Block(bool isLeaf)
		{
			IsLeaf = isLeaf;
			label = nextLabel++;
		}

		public override string ToString()
		{
			return $"{{{label}({Used}):[{string.Join(", ", data.Select(q => $"{q.Key}->{q.Link?.label}"))}]}}";
		}

		internal Block? Add(Data value)
		{
			Block? retval = null;
			var i = data.AsSpan(..Used).BinarySearch(value, comp);
			if (i >= 0)
			{
				data[i] = value;
			}
			else
			{
				if (Used < SECTOR_SIZE - 1)
				{
					var s = data.AsSpan(..(Used + 1));

					i = -i - 1;
					if (i == s.Length)
					{
						i--;
					}
					else
					{
						var src = s[i..^1];
						var dst = s[(i + 1)..];
						src.TryCopyTo(dst);
					}
					Used++;
					data[i] = value;
				}
				else
				{
					retval = Split();
					if (value.Key >= retval[0].Key)
					{
						retval.Add(value);
					}
					else
					{
						this.Add(value);
					}
				}
			}
			return retval;
		}

		internal int IndexOf(Data value) => data.AsSpan(..Used).BinarySearch(value);

		internal int GetChild(Data value)
		{
			if (IsLeaf)
				throw new InvalidOperationException("Tried to search a leaf for a child");

			var s = data.AsSpan(..Used);
			var i = s.BinarySearch(value, comp);
			if (i < 0)
				i = -i - 2;
			if (i < 0)
				i++;
			if (i == s.Length)
			{
				i--;
			}
			return i;
		}

		private Block Split()
		{
			var new_sibling = new Block(IsLeaf);
			var src = data.AsSpan((SECTOR_SIZE / 2)..);
			src.CopyTo(new_sibling.data.AsSpan());
			Used = SECTOR_SIZE / 2;
			new_sibling.Used = SECTOR_SIZE / 2 - 1;
			var link = data[SECTOR_SIZE - 1].Link;
			src.Clear();
			new_sibling[SECTOR_SIZE - 1] = new Data { Key = 0, Link = link };
			data[SECTOR_SIZE - 1] = new Data { Key = 0, Link = new_sibling };
			return new_sibling;
		}

		static void Main(string[] args)
		{
			//for (int i = 0; i < 10; i++)
			NewMethod();
		}

		private static void NewMethod()
		{
			var b = new BTree();
			var r = new Random();

			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			for (int i = 0; i < SECTOR_SIZE * 5; i++)
			{
				var k = r.Next(50) + 1;
				b.Add(new Data { Key = k });
				//b.DrawTree();
			}
			//b.Add(new Data { Key = r.Next() });
			//b.Add(new Data { Key = r.Next() });
			sw.Stop();

			var x = b.Exists(13);

			Console.WriteLine("--------------");

			foreach (var d in b.GetFrom(5,20))
			{
				Console.WriteLine(d.Key);
			}

			Console.WriteLine("--------------");
			Console.WriteLine(sw.ElapsedMilliseconds);
		}
	}
}

