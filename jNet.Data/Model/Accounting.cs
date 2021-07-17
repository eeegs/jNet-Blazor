using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Data.Model
{

	[NotMapped]
	public abstract class BaseData
	{
		private DateTime modifiedDate = DateTime.UtcNow;

		protected BaseData(string name)
		{
			Name = name;
		}

		//public AuditData(long businessId)
		//{
		//	BusinessId = businessId;
		//}
		[Key]
		public long Id { get; set; }
		public DateTime ModifiedDate {
			get => modifiedDate; 
			init => modifiedDate = value.ToUniversalTime();
		}
		public string ModifiedBy { get; init; } = "Scott";
		public string Name { get; protected set; }
	}

	[NotMapped]
	public abstract class BaseData2 : BaseData
	{
		protected BaseData2(string name) : base(name)
		{
		}
		public long BusinessId { get; internal set; }
	}

	[NotMapped]
	public abstract class BaseData3 : BaseData2
	{
		protected BaseData3(string name) : base(name)
		{
		}
		public string? Description { get; set; }
	}

	public interface IHaveCompanyDetail
	{
		CompanyDetail? Detail { get; init; }
	}

	public enum AccountType : byte
	{
		[Description("The root account for the company.")]
		Company = 0,
		[Description("Tangible and intangible items that the company owns that have value.")]
		Asset = 1,
		[Description("Money that the company owes to others.")]
		Liability = 2,
		[Description("That portion of the total assets that the owners or stockholders of the company fully own; have paid for outright.")]
		Equity = 3,
		[Description("Money the company earns from its sales of products or services, and interest and dividends earned from marketable securities.")]
		Revenue = 4,
		[Description("Money the company spends to produce the goods or services that it sells.")]
		CoGS = 5,
		[Description("Money the company spends to produce the goods or services that it sells.")]
		Expense = 6,
		[Description("Money the company earns from its sales of products or services, and interest and dividends earned from marketable securities.")]
		OtherRevenue = 7,
		[Description("Money the company spends to produce the goods or services that it sells.")]
		OtherExpence = 8,
	}
}
