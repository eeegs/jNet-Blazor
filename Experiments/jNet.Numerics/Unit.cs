using System;
using System.Linq;

namespace jNet.Numerics
{
#pragma warning disable IDE1006 // Naming Styles
	public record Unit(int kg, int m, int s, int A = 0, int K = 0, int cd = 0, int mole = 0)
	{
		private static readonly string[] symbol = new[] { "kg", "m", "s", "A", "K", "cd", "mole", "V", "Ω", "W", "H", "J", "N", "C", "Pa", "F", "Hz", "Wb", "T", "kat", "Sv", "" };
		private static readonly string[] name = new[] { "kilogram", "metre", "second", "ampere", "kelvin", "candela", "Mole", "volt", "ohm", "watt", "herry", "joule", "newton", "coulomb", "pascal", "farad", "hertz", "weber", "tesla", "katal", "sievert", "one" };
		private const string subs = "⁰¹²³⁴⁵⁶⁷⁸⁹";

		public int this[int index] => index switch
		{
			0 => kg,
			1 => m,
			2 => s,
			3 => A,
			4 => K,
			5 => cd,
			6 => mole,
			_ => 0
		};

		public static Unit Kilogram = new Unit(1, 0, 0);
		public static Unit Metre = new Unit(0, 1, 0, 0);
		public static Unit Second = new Unit(0, 0, 1, 0);
		public static Unit Ampere = new Unit(0, 0, 0, 1);
		public static Unit Kelvin = new Unit(0, 0, 0, 0, 1);
		public static Unit Candela = new Unit(0, 0, 0, 0, 0, 1);
		public static Unit Mole = new Unit(0, 0, 0, 0, 0, 0, 1);
		public static Unit Volt = new Unit(1, 2, -3, -1);
		public static Unit Ohm = new Unit(1, 2, -3, -2);
		public static Unit Watt = new Unit(1, 2, -3);
		public static Unit Herry = new Unit(1, 2, -2, -2);
		public static Unit Joule = new Unit(1, 2, -2);
		public static Unit Newton = new Unit(1, 1, -2);
		public static Unit Coulomb = new Unit(0, 0, 1, 1);
		public static Unit Pascal = new Unit(1, -1, -2);
		public static Unit Farad = new Unit(-1, -2, 4, 2);
		public static Unit Hertz = new Unit(0, 0, -1);
		public static Unit Weber = new Unit(1, 2, -2, -1);
		public static Unit Tesla = new Unit(1, 0, -2, -1);
		public static Unit Katal = new Unit(0, 0, -1, 0, 0, 0, 1);
		public static Unit Sievert = new Unit(0, 2, -2);
		public static Unit One = new Unit(0, 0, 0);

		private static Unit[] sis = new[] { Kilogram, Metre, Second, Ampere, Kelvin, Candela, Mole, Volt, Ohm, Watt, Herry, Joule, Newton, Coulomb, Pascal, Farad, Hertz, Weber, Tesla, Katal, Sievert, One };

		public override string ToString()
		{
			return string.Join(
				"·",
				Enumerable.Range(0, 7)
					.Select(q => (l: symbol[q], v: this[q]))
					.OrderByDescending(q => q.v)
					.Select(q => q switch
					{
						var x when x.v == 0 => "",
						var x when x.v == 1 => x.l,
						var x when x.v > 9 => $"{x.l}{string.Join("", x.v.ToString().Select(q => subs[q - '0']))}",
						var x when x.v > 1 => $"{x.l}{subs[x.v]}",
						var x when x.v < -9 => $"{x.l}{"⁻"}{string.Join("", (-x.v).ToString().Select(q => subs[q - '0']))}",
						var x when x.v < 0 => $"{x.l}{"⁻"}{subs[-x.v]}",
						_ => ""
					})
					.Where(q => !string.IsNullOrEmpty(q))
			);
		}
		private int Sum => Enumerable.Range(0, 7).Sum(q => Math.Abs(this[q]));

		private static Unit Pow(Unit a, int power)
		{
			return new Unit(a.kg * power, a.m * power, a.s * power, a.A * power, a.K * power, a.cd * power, a.mole * power);
		}

		public static Unit operator *(Unit a, Unit b)
		{
			return new Unit(a.kg + b.kg, a.m + b.m, a.s + b.s, a.A + b.A, a.K + b.K, a.cd + b.cd, a.mole + b.mole);
		}

		public static Unit operator /(Unit a, Unit b)
		{
			return
			 new Unit(a.kg - b.kg, a.m - b.m, a.s - b.s, a.A - b.A, a.K - b.K, a.cd - b.cd, a.mole - b.mole);
		}

		public static Measure operator *(double value, Unit unit)
		{
			return new Measure(value, unit);
		}

		public static Measure operator *(long value, Unit unit)
		{
			return new Measure(value, unit);
		}

		public static Unit operator ^(Unit unit, int power)
		{
			return Pow(unit, power);
		}

		public string asUnit(Unit target)
		{
			for (int i = 0; i < sis.Length; i++)
			{
				if (sis[i] == target)
				{
					var tmp = this / target;
					if (tmp.Sum == 0)
					{
						return symbol[i];
					}
					else
					{
						return $"{symbol[i]}{"·"}{this / target}";
					}
				}
			}
			return $"({target}){"·"}{this / target}";
		}

		public Unit Simplify()
		{
			var e = sis.Select(q => q).OrderBy(q => (this / q).Sum).First();
			return e;
		}
	}
#pragma warning restore IDE1006 // Naming Styles
}
