using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(Business))]
	public class Business : BaseData, IHaveCompanyDetail
	{
		private Business(string name) : base(name)
		{
		}

		public CompanyDetail? Detail { get; init; }

		public static Business Default { get; } = new Business("Default")
		{
			Id = -1
		};

		static public Business Create(string name)
		{
			var b = new Business(name)
			{
				Detail = new(name)
				{
					Entity = new(name)
				},
			};
			return b;
		}

		public class Config : IEntityTypeConfiguration<Business>
		{
			public void Configure(EntityTypeBuilder<Business> builder)
			{
				builder
					.HasData(
						Default
					);
			}
		}
	}
}
