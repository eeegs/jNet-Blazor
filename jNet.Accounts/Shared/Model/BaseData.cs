using System;

namespace jNet.Accounts.Shared.Model
{
	public abstract class BaseData : IHaveName
	{
		DateTime modifiedDate = DateTime.UtcNow;

		public DateTime ModifiedDate
		{
			get => modifiedDate;
			init => modifiedDate = value.ToUniversalTime();
		}
		public string ModifiedBy { get; set; } = "Scott";
		public string Name { get; set; } = "";
	}
}
