using System;

namespace jNet.Index
{
	public class LeafBlock : Block<int>
	{
		public static void Register()
		{
			Builders[255] = (a, b) => new LeafBlock(a, b);
		}

		private LeafBlock(int blockNumber, byte[] data) : base(blockNumber, data)
		{
			IsLeaf = true;
		}

		public override int Search(int item) => Data[..Used].BinarySearch(item);
		protected override int SearchInt(int item) => Data[..Used].BinarySearch(item);

		public override bool Insert(int key)
		{
			return InsertInternal(key);
		}
	}
}