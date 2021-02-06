using jNet.CRUD;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace jNet.Blazor.Shared
{
	public class Business: IHaveId<long>
	{
		[Key]
		public long Id { get; set; }

		[Required]
		[Display(Description = "This is the registered name of the business", Name = "Registered Name", ShortName = "F", Prompt = "The registered name")]
		public string Name { get; set; } = "";

		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[CreditCard]
		public string? CreditCard { get; set; }

		[Display(Description = "A Description Field", Name = "Fred", ShortName = "F", Prompt = "Hell yeah!")]
		public string Note { get; set; } = "";

		[Range(5, 10)]
		public long? ACN { get; set; }

		[Required]
		public long? ABN { get; set; }

		[Display(Name = "Business Color")]
		public Color BackgroundColor { get; set; }
	}


public class Account: IHaveId<long>
	{
		[Key]
		public long Id { get; set; }

		[Required]
		[Display(Description = "The account title", Name = "Title", Prompt = "The ledger account title")]
		public string Name { get; set; } = "";

		[Display(Description = "The account description", Name = "Fred", Prompt = "A short description of the account")]
		public string Description { get; set; } = "";

		public string Type { get; set; } = "";
		public int Direction { get; set; }
		public string? AccountNumber { get; set; }
		public long ParentId { get; set; }
		public bool IsSummaryAccount { get; set; }
	}
}

