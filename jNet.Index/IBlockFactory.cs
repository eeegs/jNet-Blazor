using System;

namespace jNet.Index
{
	public interface IBlockFactory : IDisposable
	{
		public Block Load(int blockNumber);
		public void Save();
		public T Split<T>(T oldBlock) where T: Block;
		Block Create(bool isLeaf, int blockNumber);
		Block Create(bool isLeaf);
		void SetNextNumber(Block Block);
	}
}
