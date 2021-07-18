using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(Customer))]
	public class Customer : BaseData3, IHaveCompanyDetail
	{
		public Customer(string name) : base(name)
		{
		}

		public CompanyDetail? Detail { get; init; }
		public string? ACustomerThing { get; set; }
	}
}
