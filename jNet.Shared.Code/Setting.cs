using System;

namespace jNet.Shared.Code
{
	public class Setting : BaseImpNew<Setting.Split>, IHaveKey, IHaveName
	{
		public Guid Key { get; init; } = Guid.NewGuid();
		public string UserName { get; set; } = "";
		public string Name { get; set; } = "";

		public class Split
		{
			public double SplitPosition { get; set; }
			public bool IsClosed { get; set; }
		}
	}
}


