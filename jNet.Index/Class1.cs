using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;

namespace jNet.Index
{
	public class Class1
	{
		private const string FileName = @"C:\Users\sscot\temp\index1.index";

		static void Main(string[] args)
		{
			var r = new Random(0);

			LeafBlock.Register();
			EntryBlock.Register();

			using var x = new Index2(FileName);

			x[0].IsLeaf = false;

			//var sw = new System.Diagnostics.Stopwatch();

			//for (int i = 0; i < 100000000; i++)
			//{
			//	var n = r.Next();
			//	x.Add(n);
			//}

			////System.IO.File.WriteAllLines(@"C:\Users\sscot\temp\index1.txt", x.GetFrom().Select(q => $"{q}"));

			//sw.Start();
			//Console.WriteLine($"{x.GetFrom().Sum(q => (long)q)}");
			//sw.Stop();
			//Console.WriteLine($"{sw.ElapsedMilliseconds}");
		}

		static int[] nums = new[] { 1430, 1316, 64, 192, 78, 565, 564, 1426, 725, 760, 2007, 1923, 881, 1625, 1171, 1825, 963, 1917, 13, 1097, 975, 1493, 1636, 1835, 1133, 1117, 227, 1694, 1581, 1357, 1086, 1066, 523, 968, 1796, 1276, 687, 576, 1437, 185, 292, 1922, 861, 11, 1448, 342, 995, 1394, 927, 1762, 1254, 2043, 1339, 1044, 2002, 401, 1202, 1616, 510, 574, 1050, 1839, 1402, 1311, 420, 1793, 515, 1648, 1788, 1735, 1160, 1925, 466, 19, 13, 909, 305, 1436, 324, 1583, 1826, 1406, 1592, 1162, 1285, 397, 1547, 738, 1204, 815, 637, 1715, 1524, 498, 1493, 145, 1743, 566, 2043, 417 };
	}

	public interface IDisk : IDisposable
	{
		IDiskBlock this[int blockNumber] { get; }
	}

	public class Disk : IDisk, IDisposable
	{
		readonly int _blockSize;
		readonly FileStream _file;
		MemoryMappedFile _mmf;

		public static IDisk Open(string fileName, int blockSize) => new Disk(fileName, blockSize);

		private Disk(string fileName, int blockSize)
		{
			_blockSize = blockSize;
			var path = Path.GetDirectoryName(fileName);
			if (path != null)
			{
				Directory.CreateDirectory(path);
			}

			_file = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
			if (_file.Length == 0)
			{
				_file.SetLength(blockSize * 16);
			}

			_mmf = MemoryMappedFile.CreateFromFile(_file, null, 0, MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, true);
		}

		IDiskBlock IDisk.this[int blockNumber]
		{
			get
			{
				if (blockNumber * _blockSize >= _file.Length)
				{
					_mmf.Dispose();
					_file.SetLength((blockNumber + 16) * _blockSize);
					_mmf = MemoryMappedFile.CreateFromFile(_file, null, 0, MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, true);
				}
				return new DiskBlock(_mmf.CreateViewAccessor(blockNumber * _blockSize, _blockSize), _blockSize);
			}
		}

		void IDisposable.Dispose()
		{
			_mmf.Dispose();
			_file.Dispose();
		}

		private class DiskBlock : IDiskBlock
		{
			readonly MemoryMappedViewAccessor va;
			readonly int size;

			public DiskBlock(MemoryMappedViewAccessor viewAccessor, int size)
			{
				this.size = size;
				va = viewAccessor;
			}

			T IDiskBlock.Get<T>(int offset) where T : struct
			{
				va.Read(offset, out T value);
				return value;
			}

			void IDiskBlock.Set<T>(int offset, ref T value) where T : struct => va.Write(offset, ref value);
			void IDiskBlock.Set<T>(int offset, T value) where T : struct => va.Write(offset, ref value);
			int IDiskBlock.Size => size;
			void IDisposable.Dispose() => va.Dispose();
		}
	}

	public interface IDiskBlock : IDisposable
	{
		T Get<T>(int offset) where T : struct;
		void Set<T>(int offset, T value) where T : struct;
		void Set<T>(int offset, ref T value) where T : struct;
		int Size { get; }
	}

	public class IndexBlock : IDisposable
	{
		private const byte LEAF = 255;
		private const byte NOTLEAF = 0;
		private const int TYPE = 0;
		private const int USED = 2;
		private const int NEXT = 4;
		private const int HEADER = 8;

		readonly IDiskBlock diskBlock;
		public IndexBlock(IDiskBlock block)
		{
			diskBlock = block;
		}

		public int Used
		{
			get => diskBlock.Get<int>(USED);
			set => diskBlock.Set(USED, ref value);
		}

		public bool IsLeaf
		{
			get => diskBlock.Get<byte>(TYPE) == LEAF;
			set => diskBlock.Set(USED, value ? LEAF : NOTLEAF);
		}
		public int Next
		{
			get => diskBlock.Get<int>(NEXT);
			set => diskBlock.Set(NEXT, value);
		}

		public T Get<T>(System.Index index)
			where T : struct
		{
			return diskBlock.Get<T>(HEADER + GetIndex<T>(index));
		}

		public void Set<T>(System.Index index, T value)
			where T : struct
		{
			diskBlock.Set(HEADER + GetIndex<T>(index), value);
		}

		public void Set<T>(System.Index index, ref T value)
			where T : struct
		{
			diskBlock.Set(HEADER + GetIndex<T>(index), ref value);
		}

		private int GetIndex<T>(System.Index index)
		{
			var size = Marshal.SizeOf<T>();
			var capacity = (diskBlock.Size - HEADER) / size;
			var i = index.IsFromEnd ? capacity - index.Value : index.Value;
			if (i < 0 || i >= capacity)
			{
				throw new IndexOutOfRangeException();
			}
			return i * size;
		}

		void IDisposable.Dispose() => diskBlock.Dispose();
	}

	public class Index2 : IDisposable
	{
		readonly IDisk disk;

		public Index2(string fileName, int blockSize = 4096)
		{
			disk = Disk.Open(fileName, blockSize);
		}

		public IndexBlock this[int blockNumber] => new IndexBlock(disk[blockNumber]);

		public void Dispose() => disk.Dispose();


	}
}
