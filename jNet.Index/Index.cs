using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace jNet.Index
{
	public partial class Index : IDisposable
	{
		private bool disposedValue;
		private readonly IBlockFactory Blocks;
		Block root;

		public Index(string fileName, bool readOnly = false)
		{
			Blocks = new BlockFactory(fileName, readOnly, 4096*4);
			root = Blocks.Load(-1);
		}

		public void Add(int key)
		{
			var newBlock = Add(root, key);
			if (newBlock != null)
			{
				// need to create a new root and change to ID of the old one
				EntryBlock newRoot = (EntryBlock)Blocks.Create(false, -1);
				Blocks.SetNextNumber(root);

				newRoot.Insert(new Entry(root.LeftKey, root.Number));
				newRoot.Insert(new Entry(newBlock.LeftKey, newBlock.Number));
				//Console.ForegroundColor = ConsoleColor.Yellow;
				//Console.WriteLine($"New Root pointing to {root.Number} & {newBlock.Number}");
				root = newRoot;
			}
		}

		private Block? Add(Block current, int key)
		{
			if (current.IsLeaf)
			{
				// at the bottom so just add it
				return TryAdd((LeafBlock)current, key);
			}

			// else figure out which child we need to work on
			var block = (EntryBlock)current;
			var childIndex = block.Search(key);
			if (childIndex < 0)
			{
				childIndex = -childIndex - 2;
			}

			if (childIndex < 0)
			{
				// key is < leftkey so lower the left key to 'key'
				block.Data[0] = new(key, block.Data[0].BlockNumber);
				childIndex = 0;
			}

			var childData = block.Data[childIndex];
			var child = Blocks.Load(childData.BlockNumber);

			// try an add the key to the child, which may return a new Block
			var newBlock = Add(child, key);
			if (newBlock != null)
			{
				// a new block needs to be added to the current
				return TryAdd(block, new(newBlock.LeftKey, newBlock.Number));
			}
			return null;
		}

		private LeafBlock? TryAdd(LeafBlock block, int key)
		{
			if (block.Insert(key))
			{
				return null;
			}

			var newBlock = Blocks.Split(block);
			if (key < newBlock.LeftKey)
			{
				block.Insert(key);
			}
			else
			{
				newBlock.Insert(key);
			}
			return newBlock;
		}

		private EntryBlock? TryAdd(EntryBlock block, Entry entry)
		{
			if (block.Insert(entry))
			{
				return null;
			}

			var newBlock = Blocks.Split(block);
			if (entry.Key < newBlock.LeftKey)
			{
				block.Insert(entry);
			}
			else
			{
				newBlock.Insert(entry);
			}
			return newBlock;
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
					Console.Write(cur.Number + " ");
					cur = Blocks.Load(cur.NextBlock);
				} while (cur.Number != -1);
				Console.WriteLine();
				var down = (left as EntryBlock)?.Data[0].BlockNumber ?? -1;
				left = Blocks.Load(down);
			} while (left.Number != -1);
		}

		public void DrawLinks()
		{
			if (root is LeafBlock) return;

			Block blk;
			var cur = (EntryBlock)root;
			do
			{
				blk = Blocks.Load(cur.Data[0].BlockNumber);
				cur = blk as EntryBlock;
			} while (cur is not null);

			var left = (LeafBlock)blk;
			do
			{
				Console.Write($"{left.Number} ");
				left = Blocks.Load(left.NextBlock) as LeafBlock;
			} while (left is not null);
			Console.WriteLine();
		}

		public IEnumerable<int> GetFrom()
		{
			var left = Blocks.Load(1) as LeafBlock;

			while (left != null)
			{
				for (int i = 0; i < left.Used; i++)
				{
					yield return left.Data[i];
				}
				left = Blocks.Load(left.NextBlock) as LeafBlock;
			};
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
					Blocks.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~Index()
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
