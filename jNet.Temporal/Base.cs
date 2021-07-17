using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace jNet.Temporal
{
	public enum SaveState
	{
		New,
		Original,
		Modified
	}

	public abstract class Base : IEnumerable<string>
	{
		internal SaveState state = SaveState.New;
		protected readonly Dictionary<string, string> entries = new();
		internal HashSet<string>? ModifiedFileds;

		public Guid Id { get; internal set; }
		internal protected DateTimeOffset? Start { get; init; }
		internal protected DateTimeOffset? End { get; init; }
		internal protected string Category { get; init; }

		internal protected string Type { get ; init; }

		public Base(string type)
		{
			Type = type;
			Category = GetType().Name;
		}

		public int Count => entries.Count;

		protected string Get([CallerMemberName] string key = "") => entries[key];
		protected void Set(string value, [CallerMemberName] string key = "") => this[key] = value;

		public string this[string key]
		{
			get => entries[key];
			set
			{
				switch (state)
				{
					case SaveState.New:
					case SaveState.Modified:
						entries[key] = value;
						break;
					case SaveState.Original:
						if (entries.TryGetValue(key, out var curVal))
						{
							if (curVal != value)
							{
								entries[key] = value;
								state = SaveState.Modified;
								ModifiedFileds ??= new();
								ModifiedFileds.Add(key);
							}
						}
						break;
				}
			}
		}

		public bool ExistsOn(DateTime date) => ExistsBetween(date, date);
		public bool Exists() => ExistsOn(DateTime.Now);
		public bool ExistsBetween(DateTime? date1, DateTime? date2)
		{
			var a = Start is null || date2 is null || Start.Value.LocalDateTime.Date <= date2.Value.Date;
			if (!a) return false;
			var b = End is null || date1 is null || End.Value.LocalDateTime.Date >= date1.Value.Date;
			return b;
		}

		public bool ExistsWith(Base entity) => ExistsBetween(entity.Start?.LocalDateTime, entity.End?.LocalDateTime); 

		public bool ContainsKey(string key) => entries.ContainsKey(key);

		public bool Remove(string key) => entries.Remove(key);

		public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value) => entries.TryGetValue(key, out value);

		public void Clear() => entries.Clear();

		public IEnumerator<string> GetEnumerator() => entries.Keys.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => entries.Keys.GetEnumerator();
	}
}
