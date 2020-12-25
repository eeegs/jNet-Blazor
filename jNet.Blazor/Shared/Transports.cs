using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Blazor.Shared
{
	public class Business
	{
		[Key]
		public long Id { get; set; }

		[Required]
		[ScaffoldColumn(false)]
		[Display(Description = "This is the registered name of the business", Name = "Registered Name", ShortName = "F", Prompt = "The registered name")]
		public string Name { get; set; } = "";

		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[CreditCard]
		public string? CreditCard { get; set; }

		[Display(Description = "A Description Field", Name = "Fred", ShortName = "F", Prompt = "Hell yeah!")]
		public string Description { get; set; } = "";

		[Range(5, 10)]
		public long? ACN { get; set; }

		[Required]
		public long? ABN { get; set; }
		public long AccountId { get; set; }

		[Display(Name = "Business Color")]
		public Color BackgroundColor { get; set; }
	}
}
