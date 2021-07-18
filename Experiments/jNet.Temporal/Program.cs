using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace jNet.Temporal
{
	public class Gap : Node
	{
		public Gap(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
		}
	}

	public class Person : Node
	{
		public string FirstName { get => Get(); set => Set(value); }
		public string LastName { get => Get(); set => Set(value); }

		public DateTimeOffset? Born { get => Start; init => Start = value; }
		public DateTimeOffset? Died { get => End; init => End = value; }
	}

	public class Married : Edge
	{
		public Married(Person a, Person b) : base(a, b, true) { }
		public string Location { get => Get(); set => Set(value); }

		public DateTimeOffset? Date { get => Start; init => Start = value; }
		public DateTimeOffset? Devorced { get => End; init => End = value; }
	}

	class Program
	{
		static void Main4(string[] args)
		{
			var s = new Person
			{
				Born = new(1966, 8, 19, 12, 21, 0, TimeSpan.FromHours(10)),
				FirstName = "Scott",
				LastName = "Egan"
			};

			var j = new Person
			{
				Born = new(1965, 7, 16, 7, 30, 0, TimeSpan.FromHours(10)),
				FirstName = "Julie",
				LastName = "Egan",
			};

			var m = new Married(s, j)
			{
				Date = new(1988, 10, 1, 16, 0, 0, TimeSpan.FromHours(10)),
				Location = "Duntroon Chapel",
			};

			var g = new Gap(new DateTime(1965, 1, 1), new DateTime(1965, 8, 1, 8, 30, 30));

			var db = new Graph();

			db.Nodes.Add(s, j, g);
			db.Edges.Add(m);
			db.Save();
		}




		static void Main2(string[] args)
		{
			long offset = 0x10000000; // 256 megabytes
			long length = 0x20000000; // 512 megabytes

			// Create the memory-mapped file.
			using (var mmf = MemoryMappedFile.CreateFromFile(@"C:\Users\sscot\ExtremelyLargeImage.data", FileMode.OpenOrCreate, "ImgA", (long)0x40000000 * Marshal.SizeOf(typeof(MyColor))))
			{
				// Create a random access view, from the 256th megabyte (the offset)
				// to the 768th megabyte (the offset plus length).
				using (var accessor = mmf.CreateViewAccessor(offset, length))
				{
					int colorSize = Marshal.SizeOf(typeof(MyColor));
					MyColor color;

					// Make changes to the view.
					for (long i = 0; i < length; i += colorSize)
					{
						accessor.Read(i, out color);
						color.Brighten(10);
						accessor.Write(i, ref color);
					}
				}
			}
		}


		public struct MyColor
		{
			public short Red;
			public short Green;
			public short Blue;
			public short Alpha;

			// Make the view brighter.
			public void Brighten(short value)
			{
				Red = (short)Math.Min(short.MaxValue, (int)Red + value);
				Green = (short)Math.Min(short.MaxValue, (int)Green + value);
				Blue = (short)Math.Min(short.MaxValue, (int)Blue + value);
				Alpha = (short)Math.Min(short.MaxValue, (int)Alpha + value);
			}
		}
	}
}

