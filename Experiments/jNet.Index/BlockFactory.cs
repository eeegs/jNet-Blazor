using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jNet.Index
{
	public class BlockFactory : IBlockFactory, IDisposable
	{
		public readonly int SECTOR_SIZE;
		private readonly Dictionary<int, Block> blocks = new();
		private readonly Dictionary<int, Block> dirtyBlocks = new();
		private bool disposedValue;
		private readonly FileStream file;
		private int blockCount = 0;


		public BlockFactory(string fileName, int blockSize) : this(fileName, false, blockSize) { }
		public BlockFactory(string fileName, bool readOnly) : this(fileName, readOnly, 4096) { }
		public BlockFactory(string fileName, bool readOnly = false, int blockSize = 4096)
		{
			SECTOR_SIZE = blockSize;
			var path = Path.GetDirectoryName(fileName);
			if (path != null)
			{
				Directory.CreateDirectory(path);
			}
			readOnly &= File.Exists(fileName);
			file = File.Open(fileName, FileMode.OpenOrCreate, readOnly ? FileAccess.Read : FileAccess.ReadWrite, FileShare.Read);
			var blocks = (int)(file.Length / SECTOR_SIZE);
			if (blocks > 0)
			{
				blockCount = blocks - 1;
			}
			else
			{
				//Contract.Requires<InvalidOperationException>(!readOnly,"Can't create a new index with a readonly file");
				blockCount = 0;
				var blk = Block.New(-1, true, SECTOR_SIZE);
				Add(blk);
			}
		}

		public void SetNextNumber(Block block)
		{
			block.Number = blockCount++;
			Add(block);
		}

		public Block Load(int blockNumber)
		{
			if (blocks.TryGetValue(blockNumber, out var block))
			{
				return block;
			}

			var bytes = new byte[SECTOR_SIZE];
			file.Seek(blockNumber * SECTOR_SIZE, blockNumber < 0 ? SeekOrigin.End : SeekOrigin.Begin);
			file.Read(bytes);
			var res = Block.Load(blockNumber, bytes);
			Add(res);
			return res;
		}

		private void Add(Block block)
		{
			blocks[block.Number] = block;
			if (block.IsDirty)
			{
				dirtyBlocks[block.Number] = block;
			}
		}

		public void Save()
		{
			if (dirtyBlocks.Count == 0) return;

			var list = dirtyBlocks.Keys.ToList();
			list.Sort();
			list.Remove(-1);


			list.ForEach(i =>
			{

				var block = dirtyBlocks[i];
				file.Seek(i * SECTOR_SIZE, SeekOrigin.Begin);
				file.Write(block.Bytes);
				block.IsDirty = false;
			});

			// always write root
			if (blocks.TryGetValue(-1, out var block))
			{
				file.Seek(blockCount * SECTOR_SIZE, SeekOrigin.Begin);
				file.Write(block.Bytes);
				block.IsDirty = false;
			}
			dirtyBlocks.Clear();
		}

		public T Split<T>(T oldBlock)
			where T : Block
		{
			var newBlock = (T)Create(oldBlock.IsLeaf);       // make a block of the same type

			var move = oldBlock.CAPACITY / 2;
			var leave = oldBlock.CAPACITY - move;
			var size = (SECTOR_SIZE - Block.CONTROL_SIZE) / oldBlock.CAPACITY;

			// move the last bytes (less control) to the new block)
			var srcSpan = oldBlock.Bytes.AsSpan((leave * size)..^Block.CONTROL_SIZE);
			srcSpan.CopyTo(newBlock.Bytes);
			srcSpan.Clear();

			// update the control data
			newBlock.Used = move;
			newBlock.NextBlock = oldBlock.NextBlock;
			oldBlock.Used = leave;
			oldBlock.NextBlock = newBlock.Number;

			//Console.ForegroundColor = ConsoleColor.Green;
			//Console.WriteLine($"Splitting {oldBlock.Number} into {newBlock.Number}");


			return newBlock;
		}

		public Block Create(bool isLeaf) => Create(isLeaf, blockCount++);
		public Block Create(bool isLeaf, int blockNumber)
		{
			var res = Block.New(blockNumber, isLeaf, SECTOR_SIZE);
			if (res is null)
			{
				throw new InvalidOperationException("How did this happen");
			}
			Add(res);
			return res!;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// Dispose of any managed state (managed objects)
					Save();
					file.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~BlockFactory()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}


