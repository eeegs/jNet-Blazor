using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Data.Model
{
	public interface IAmTemporal
	{
		DateTime? Started();
		DateTime? Ended();
		TimeSpan? Duration();
	}

	[Table("Root")]
	public abstract class Root : IAmTemporal
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime? Start { get; init; }
		public DateTime? End { get; init; }
		public virtual TimeSpan? Duration()
		{
			return End - Start;
		}

		public virtual DateTime? Ended() => End;
		public virtual DateTime? Started() => Start;
	}

	[Table("Entity")]
	public abstract class Entity : Root
	{
		public IList<Entity> LeftEntities { get; set; } = new List<Entity>();
		public IList<Entity> RightEntities { get; set; } = new List<Entity>();
		public IList<Association> LeftAssociations { get; set; } = new List<Association>();
		public IList<Association> RightAssociations { get; set; } = new List<Association>();


		public class baseConfig : IEntityTypeConfiguration<Entity>
		{
			public void Configure(EntityTypeBuilder<Entity> builder)
			{
				builder
					.HasMany(m => m.RightEntities)
					.WithMany(m => m.LeftEntities)
					.UsingEntity<Association>(
						j => j
							.HasOne(b => b.Right)
							.WithMany(b => b.LeftAssociations)
							.HasForeignKey(b => b.RightId),
						j => j
							.HasOne(m => m.Left)
							.WithMany(m => m.RightAssociations)
							.HasForeignKey(m => m.LeftId)
					   );
			}
		}
	}

	[Table("Association")]
	public class Association : Root
	{
		public override DateTime? Started()
		{
			var t = (Start, Left?.Start, Right?.Start);
			var result = t switch
			{
				(not null, _, _) => t.Item1,
				(null, null, _) => t.Item3,
				(null, _, null) => t.Item2,
				(null, not null, not null) when t.Item2 < t.Item3 => t.Item3,
				(null, not null, not null) => t.Item2,
			};
			return result;
		}
		public override DateTime? Ended()
		{
			var t = (End, Left?.End, Right?.End);
			var result = t switch
			{
				(not null, _, _) => t.Item1,
				(null, null, _) => t.Item3,
				(null, _, null) => t.Item2,
				(null, not null, not null) when t.Item2 > t.Item3 => t.Item3,
				(null, not null, not null) => t.Item2,
			};
			return result;
		}

		public override TimeSpan? Duration()
		{
			return Ended() - Started();
		}

		public bool Active() => Active(DateTime.Now);

		private bool Active(DateTime date)
		{
			return (Started(), Ended()) switch
			{
				(null, null) => true,
				(null, _) x when date < x.Item2 => true,
				(_, null) x when x.Item1 > date => true,
				(_, _) x when x.Item1 > date && date < x.Item2 => true,
				_ => false
			};
		}

		public int LeftId { get; init; }
		public Entity Left { get; init; }
		public int RightId { get; init; }
		public Entity Right { get; init; }
		public string Type { get; init; }
		public class Config : IEntityTypeConfiguration<Association>
		{
			public void Configure(EntityTypeBuilder<Association> builder)
			{
				builder
					.HasData(
						new Association { Id = -6, RightId = -3, LeftId = -1, Type = "Child" },
						new Association { Id = -7, RightId = -3, LeftId = -2, Type = "Child" },
						new Association { Id = -8, RightId = -4, LeftId = -1, Type = "Child" },
						new Association { Id = -9, RightId = -4, LeftId = -2, Type = "Child" },
						new Association { Id = -10, RightId = -5, LeftId = -1, Type = "Child" },
						new Association { Id = -11, RightId = -5, LeftId = -2, Type = "Child" },
						new Association { Id = -12, RightId = -1, LeftId = -2, Type = "Spouse", Start = new DateTime(1988, 10, 1) },
						new Association { Id = -13, RightId = -1, LeftId = -6, Type = "Child" }
					 );
			}
		}
	}


	[Table("Person")]
	public class Person : Entity
	{
		public string Name { get; init; } = "";
		public class Config : IEntityTypeConfiguration<Person>
		{
			public void Configure(EntityTypeBuilder<Person> builder)
			{
				builder
					.HasData(
						new Person { Id = -1, Name = "Scott", Start = new DateTime(1966, 8, 19, 12, 20, 0) },
						new Person { Id = -2, Name = "Julie" },
						new Person { Id = -3, Name = "Matthew" },
						new Person { Id = -4, Name = "Melissa" },
						new Person { Id = -5, Name = "William", Start = new DateTime(1996, 3, 26, 7, 15, 0) },
						new Person { Id = -6, Name = "Anthony", Start = new DateTime(1940, 2, 29), End = new DateTime(1990, 9, 6) }
					);
			}
		}
	}
}
