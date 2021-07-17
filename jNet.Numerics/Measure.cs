using System;

namespace jNet.Numerics
{
	public record Measure(double Value, Unit Unit)
	{
		public Measure(double value): this(value, Unit.One)
		{
		}

		public override string ToString()
		{
			return $"{Value} {Unit}";
		}

		public static implicit operator double(Measure siNumber) => siNumber.Value;
		public static implicit operator Measure(double value) => new Measure(value);

		public static Measure operator +(Measure a, Measure b)
		{
			if (a.Unit != b.Unit)
			{
				throw new InvalidOperationException($"Cannot add two Measures with different units ('{a.Unit}' and '{b.Unit}').");
			}
			return new Measure(a.Value + b.Value, a.Unit);
		}

		public static Measure operator -(Measure a, Measure b)
		{
			if (a.Unit != b.Unit)
			{
				throw new InvalidOperationException($"Cannot subtract two Measures with different units ('{a.Unit}' and '{b.Unit}').");
			}
			return new Measure(a.Value - b.Value, a.Unit);
		}

		public static Measure operator -(Measure a) => new Measure(-a.Value, a.Unit);

		public static Measure operator *(Measure a, Measure b)
		{
			return new Measure(a.Value * b.Value, a.Unit * b.Unit);
		}

		public static Measure operator /(Measure a, Measure b)
		{
			return new Measure(a.Value / b.Value, a.Unit / b.Unit);
		}

		public static bool operator <(Measure a, Measure b)
		{
			if (a.Unit != b.Unit)
			{
				throw new InvalidOperationException($"Cannot compare two Measures with different units ('{a.Unit}' and '{b.Unit}').");
			}
			return a.Value < b.Value;
		}

		public static bool operator >(Measure a, Measure b)
		{
			if (a.Unit != b.Unit)
			{
				throw new InvalidOperationException($"Cannot compare two Measures with different units ('{a.Unit}' and '{b.Unit}').");
			}
			return a.Value > b.Value;
		}

		public Measure Do(Func<double, double> func)
		{
			return this with { Value = func(this.Value) };
		}

		public string asUnit(Unit target) => $"{Value} {Unit.asUnit(target)}";

		public string asUnit() => asUnit(this.Unit.Simplify());
	}
}
