using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(Supplier))]
	public class Supplier : BaseData3, IHaveCompanyDetail
	{
		public Supplier(string name) : base(name)
		{
		}
		public CompanyDetail? Detail { get; init; }
		public string? ASupplierThing { get; set; }
	}
}
