using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Shared
{
	public class Setting : BagNew<Setting.Split>, IHaveKey, IHaveName
	{
		public string Key { get; init; } = Guid.NewGuid().ToString();
		public string UserName { get; set; } = "";
		public string Name { get; set; } = "";

		public class Split
		{
			public double SplitPosition { get; set; }
			public bool IsClosed { get; set; }
		}
	}
}
