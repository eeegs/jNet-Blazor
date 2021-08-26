using System.ComponentModel;

namespace jNet.Accounts.Shared.Model
{
	public enum AccountType : byte
	{
		[Description("The root account for the company.")]
		Root = 0,
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
		OtherExpense = 8,
	}
}
