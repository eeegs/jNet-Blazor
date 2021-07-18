using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace jNet.Index
{
	public class EntryBlock : Block<Entry>
	{
		readonly static IComparer<Entry> Comparer = new ValueComparer();

		public static void Register()
		{
			Builders[0] = (a, b) => new EntryBlock(a, b);
		}

		private EntryBlock(int blockNumber, byte[] data) : base(blockNumber, data)
		{
			IsLeaf = false;
		}

		public override int Search(int key) => Data[..Used].BinarySearch(new(key), Comparer);
		protected override int SearchInt(Entry item) => Data[..Used].BinarySearch(item, Comparer);

		public override bool Insert(int key)
		{
			return InsertInternal(new(key));
		}

		public bool Insert(Entry entry)
		{
			return InsertInternal(entry);
		}

		private struct ValueComparer : IComparer<Entry>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int Compare(Entry a, Entry b) => a.Key - b.Key;
		}
	}
}