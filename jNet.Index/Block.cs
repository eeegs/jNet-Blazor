using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace jNet.Index
{
	public abstract class Block
	{
		public const int CONTROL_SIZE = 8;
		protected readonly int STORAGE_SIZE;
		protected const int LEAF_BYTE = CONTROL_SIZE;
		protected const int USED_SHORT = 6;
		protected const int NEXT_INT = 4;

		protected static Dictionary<int, Func<int, byte[], Block>> Builders = new();

		public int Number { get; set; } = -1;
		public readonly byte[] Bytes;
		public abstract int CAPACITY { get; }
		public bool IsDirty { get; set; } = true;


		public static Block Load(int blockNumber, byte[] bytes)
		{
			if (Builders.TryGetValue(bytes[^LEAF_BYTE], out var func))
			{
				var res = func(blockNumber, bytes);
				res.IsDirty = false;
				return res;
			}
			throw new InvalidOperationException("How did this happen");
		}

		public static Block New(int blockNumber, bool isLeaf, int size)
		{
			if (Builders.TryGetValue(isLeaf ? 255 : 0, out var func))
			{
				var res = func(blockNumber, new byte[size]);
				res.NextBlock = -1;
				res.IsDirty = true;
				return res;
			}
			throw new InvalidOperationException("How did this happen");
		}

		protected Block(int blockNumber, byte[] data)
		{
			Bytes = data;
			Number = blockNumber;
			STORAGE_SIZE = data.Length - CONTROL_SIZE;
		}

		protected Block(byte[] data)
		{
			Bytes = data;
		}

		public int NextBlock
		{
			get => MemoryMarshal.Cast<byte, int>(Bytes.AsSpan(^NEXT_INT..))[0];
			set => MemoryMarshal.Cast<byte, int>(Bytes.AsSpan(^NEXT_INT..))[0] = value;
		}

		public int Used
		{
			get => MemoryMarshal.Cast<byte, short>(Bytes.AsSpan(^USED_SHORT..))[0];
			set => MemoryMarshal.Cast<byte, short>(Bytes.AsSpan(^USED_SHORT..))[0] = (short)value;
		}

		public bool IsLeaf
		{
			get => Bytes[^LEAF_BYTE] == 255;
			protected set => Bytes[^LEAF_BYTE] = value ? (byte)255 : (byte)0;
		}

		public abstract bool Insert(int key);

		public abstract int Search(int key);

		public int LeftKey => MemoryMarshal.Cast<byte, int>(Bytes.AsSpan())[0];
	}

	public abstract class Block<T> : Block
		where T : struct
	{
		readonly int capacity;
		readonly static int itemsize = Marshal.SizeOf<T>();

		protected Block(int blockNumber, byte[] data) : base(blockNumber, data)
		{
			capacity = STORAGE_SIZE / itemsize;
		}

		protected abstract int SearchInt(T item);
		public override string ToString() => $"{{{Number}:{Used}:{NextBlock}}}";

		public Span<T> Data => (MemoryMarshal.Cast<byte, T>(Bytes))[..capacity];
		public override int CAPACITY => capacity;

		protected bool InsertInternal(T item)
		{
			IsDirty = true;

			var index = SearchInt(item);
			if (index < 0)
			{
				if (Used == capacity)
				{
					return false;
				}
				index = -index - 1;
				var s = Data[..(Used + 1)];
				var src = s[index..^1];
				var dst = s[(index + 1)..];
				src.TryCopyTo(dst);
				Data[index] = item;
				Used++;
			}
			else
			{
				Data[index] = item;
			}
			return true;
		}
	}
}