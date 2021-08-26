using System;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Accounts.Shared.Model
{
	public class Transaction : BaseData, IHaveKey<Guid>
	{
		private DateTimeOffset transactionDate;

		public Transaction(string name)
		{
			Name = name;
			Key = Guid.NewGuid();
			TransactionDate = DateTimeOffset.Now;
		}

		public Guid Key { get; init; }
		public string? Note { get; set; }
		public int FY { get; private set; }

		public DateTimeOffset TransactionDate
		{
			get => transactionDate;
			set
			{
				transactionDate = value;
				FY = transactionDate.LocalDateTime.AddMonths(6).Year;
			}
		}
		public List<Entry> Entries { get; init; } = new();

		public bool IsBalanced => Entries.Sum(q => q.Amount * (int)q.Type) == 0;

		public static Transaction ReceivePayment(string name, DateTime date, decimal amount, Account credit, Account debit, TaxEntry ctax, TaxEntry dtax)
		{
			var tx = new Transaction(name)
			{
				TransactionDate = date,
			};

			var creditAmount = amount / 10000;
			var creditTax = creditAmount * ctax.Formula;
			var fullAmount = creditAmount + creditTax;
			var debitAmount = fullAmount / (1 + dtax.Formula);
			var debitTax = fullAmount - debitAmount;

			if (creditTax != 0) tx.Entries.Add(new(ctax.DebitAccountKey, creditTax, "NONE") { Type = EntryType.Credit });
			if (debitTax != 0) tx.Entries.Add(new(dtax.CreditAccountKey, debitTax, "NONE") { Type = EntryType.Debit });
			tx.Entries.Add(new(credit.Key, creditAmount, ctax.Key) { Type = EntryType.Credit });
			tx.Entries.Add(new(debit.Key, debitAmount, dtax.Key) { Type = EntryType.Debit });
			return tx;
		}
	}
}
