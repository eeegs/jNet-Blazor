using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace jNet.Index
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Entry
	{
		public Entry(int key, int blockNum = 0)
		{
			Key = key;
			BlockNumber = blockNum;
		}
		public int Key;
		public int BlockNumber;

		public override string ToString()
		{
			return $"{Key}->[{BlockNumber}]";
		}
	}
}
