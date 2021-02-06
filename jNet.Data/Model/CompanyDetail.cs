using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(CompanyDetail))]
	public class CompanyDetail : BaseData
	{
		internal CompanyDetail(string name) : base(name)
		{
		}

		public long? ACN { get; set; }
		public long? ABN { get; set; }
		public string? Note { get; set; }
		public Entity? Entity { get; init; }
		public Business? Business { get; init; }
		public Supplier? Supplier { get; init; }
		public Customer? Customer { get; init; }

		public static CompanyDetail Default { get; } = new CompanyDetail("Default")
		{
			Id = -1,
		};

		public class Config : IEntityTypeConfiguration<CompanyDetail>
		{
			public void Configure(EntityTypeBuilder<CompanyDetail> builder)
			{
				builder.HasKey(q => q.Id);
				builder
					.HasOne(q => q.Business)
					.WithOne(q => q!.Detail!)
					.HasForeignKey<Business>(q => q.Id);
				builder
					.HasOne(q => q.Supplier)
					.WithOne(q => q!.Detail!)
					.HasForeignKey<Supplier>(q => q.Id);
				builder
					.HasOne(q => q.Customer)
					.WithOne(q => q!.Detail!)
					.HasForeignKey<Customer>(q => q.Id);
				builder
					.HasData(
						Default
				);
			}
		}
	}

	[Table(nameof(Entity))]
	public class Entity : BaseData
	{
		public CompanyDetail? Detail { get; init; }
		public Employee? Employee { get; init; }
		internal Entity(string name) : base(name)
		{
		}

		public static Entity Default { get; } = new Entity("Default")
		{
			Id = -1,
		};


		public class Config : IEntityTypeConfiguration<Entity>
		{
			public void Configure(EntityTypeBuilder<Entity> builder)
			{
				builder.HasKey(q => q.Id);
				builder
					.HasOne(q => q.Detail)
					.WithOne(q => q!.Entity!)
					.HasForeignKey<CompanyDetail>(q => q.Id);
				builder
					.HasOne(q => q.Employee)
					.WithOne(q => q!.Entity!)
					.HasForeignKey<Employee>(q => q.Id);
				builder
					.HasData(
						Default
				);
			}
		}
	}
}
