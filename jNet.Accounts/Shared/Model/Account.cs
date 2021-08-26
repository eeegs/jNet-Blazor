using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Shared.Model
{

	public class Account : BaseData, IHaveKey<Guid>
	{
		public Account()
		{
		}

		public Account(string name, Account? parent, EntryType? entryType = null, string? description = null)
		{
			Name = name;
			Description = description;
			if (parent is not null)
			{
				ParentKey = parent.Key;
				Type = parent.Type;
				parent.IsSummaryAccount = true;
				DefaultEntryType = entryType ?? parent.DefaultEntryType;
			}
			else
			{
				DefaultEntryType = entryType ?? EntryType.Credit;
			}
		}

		public Guid Key { get; init; } = Guid.NewGuid();
		public string? Description { get; set; }
		public AccountType Type { get; set; }
		public EntryType DefaultEntryType { get; set; }

		[Required]
		public string AccountNumber { get; set; } = "";
		public Guid ParentKey { get; set; }
		public bool IsSummaryAccount { get; set; }
		public string? TaxTypeKey { get; set; }
		public string Specialisation { get; set; } = "";
		public decimal Balance { get; set; }
	}
}
