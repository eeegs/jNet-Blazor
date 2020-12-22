using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Blazor.Shared
{
	public class Business
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ACN { get; set; }
		public string ABN { get; set; }
		public long AccountId { get; set; }
	}

	public class FormData
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool ReadOnly { get; set; }
		public ICollection<FieldDef> FieldData { get; set; } = new List<FieldDef>();
	}

	public class FieldDef
	{
		public int Index { get; set; }
		public int Columns { get; set; }
	}

	public class LineBreak : FieldDef
	{
	}

	public class InputField : FieldDef
	{
		public string Name { get; set; }
		public string Lable { get; set; }
		public string Description { get; set; }
		public InputType InputType { get; set; }
	}

	public enum InputType : byte
	{
		Button,
		Checkbox,
		Color,
		Date,
		DatetimeLocal,
		Email,
		File,
		Hidden,
		Image,
		Month,
		Number,
		Password,
		Radio,
		Range,
		Reset,
		Search,
		Submit,
		Tel,
		Text,
		Time,
		Url,
		Week
	}
}
