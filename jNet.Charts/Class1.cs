using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace jNet.Charts
{
	public class ASet<T> : SortedSet<T>
	{
		public ASet(Func<T, T, int> compare) : base(new Comp())
		{
			((Comp)base.Comparer)._compare = compare;
		}

		public T this[int index] => this.Skip(index).First();

		private class Comp : IComparer<T>
		{
			internal Func<T, T, int> _compare = default!;

			int IComparer<T>.Compare(T? x, T? y)
			{
				return (x, y) switch
				{
					(null, not null) => -1,
					(not null, null) => 1,
					(null, null) => 0,
					_ => _compare(x!, y!)
				};
			}
		}
	}

	public abstract class HasFormat
	{
		private Format? _format = null;
		public Format Format
		{
			get => _format ?? ParentFormatObject?.Format ?? Format.DefaultFormat;
			set => _format = value;
		}

		internal HasFormat? ParentFormatObject { get; set; }
	}

	public class Chart : HasFormat, IEnumerable<Series>
	{
		static int i = 0;

		public string Title { get; set; } = $"Chart{++i}";

		internal protected readonly ASet<DataValue> _range = new ASet<DataValue>((x, y) => x.Value.CompareTo(y.Value));
		public IEnumerable<DataValue> Range => _range;

		private readonly ASet<Series> _series = new ASet<Series>((x, y) => x.Order.CompareTo(y.Order));
		public void AddSeries<TRange, TVlaues>(string name, params PointData<TRange, TVlaues>[] dataSet) => AddSeries(name, dataSet);
		public void AddSeries<TRange, TVlaues>(string name, IEnumerable<PointData<TRange, TVlaues>> dataSet)
		{
			var s = new Series(name);
			{
				s.ParentFormatObject = this;
			}

			foreach (var pt in dataSet)
			{
				pt.RangeValue.ParentFormatObject = this;
				pt.SeriesValue.ParentFormatObject = s;
				_range.Add(pt.RangeValue);
				s[pt.RangeValue] = pt.SeriesValue;
			}
			lock (_series)
			{
				_series.Add(s);
				s.Order = _series.Count;
			}
		}

		public IEnumerator<Series> GetEnumerator()
		{
			return ((IEnumerable<Series>)_series).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_series).GetEnumerator();
		}

		public Series this[string name] => _series.Single(q => q.Name == name);
		public Series this[int index] => _series[index];

	}

	public record PointData(RangeValue RangeValue, SeriesValue SeriesValue);

	public record PointData<TRange, TVlaues>(RangeValue<TRange> RangeValue, SeriesValue<TVlaues> SeriesValue);

	public class Series : HasFormat, IEnumerable<PointData>
	{
		private Dictionary<RangeValue, SeriesValue> _values = new();

		internal Series(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
		public int Order { get; set; } = 0;

		public SeriesValue? this[RangeValue index]
		{
			get
			{
				if (_values.TryGetValue(index, out var result))
				{
					return result;
				}
				return null;
			}
			set => _values[index] = value ?? default!;
		}

		public IEnumerator<PointData> GetEnumerator()
		{
			return _values.Select(q => new PointData(q.Key, q.Value)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public abstract class DataValue : HasFormat
	{
		protected abstract object? valueAsObject { get; }

		public double Value => this;

		static public implicit operator double(DataValue value) => value.valueAsObject switch
		{
			sbyte v => v,
			byte v => v,
			int v => v,
			uint v => v,
			short v => v,
			ushort v => v,
			long v => v,
			ulong v => v,
			decimal v => (double)v,
			float v => v,
			double v => v,
			DateTime v => v.Ticks / 10e6,       // 1 tick = 100ns, ∴ / 10 million to get seconds
			DateTimeOffset v => v.UtcTicks / 10e6,
			TimeSpan v => v.TotalSeconds,
			char v => char.GetNumericValue(v),
			_ => 0
		};
	}

	public abstract class RangeValue : DataValue
	{
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}

	public abstract class SeriesValue : DataValue
	{
	}

	public class SeriesValue<T> : SeriesValue
	{
		T _value;

		public SeriesValue(T value)
		{
			_value = value;
		}

		protected sealed override object? valueAsObject => _value;
		public override string ToString() => Format.ToString(_value);

		static public implicit operator SeriesValue<T>(T value) => new SeriesValue<T>(value);
	}

	public class RangeValue<T> : RangeValue
	{
		T _value;

		public RangeValue(T value)
		{
			_value = value;
		}


		protected sealed override object? valueAsObject => _value;
		public override string ToString() => Format.ToString(_value);

		static public implicit operator RangeValue<T>(T value) => new RangeValue<T>(value);
	}

	public record Format(string FormatString)
	{
		internal static Format DefaultFormat = new Format
		{
		};

		internal Format() : this("") { }

		public Color Color { get; init; } = Color.Black;
		public Color BacgroundColor { get; init; } = Color.Transparent;
		public string ToString(object? value) => string.Format($"{{0:{FormatString}}}", value);
		public override string ToString() => FormatString;
	}
}
