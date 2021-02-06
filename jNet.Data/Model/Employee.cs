using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Data.Model
{
	[Table(nameof(Employee))]
	public class Employee: BaseData3
	{
		public Employee(string name) : base(name)
		{
		}

		public DateTime StartDate { get; set; }
		public Entity? Entity { get; init; }

		public enum Contract
		{
			Perminent = 1,
			Fixed
		}

		public enum Enguagement
		{
			FullTime = 1,
			PartTime,
			Casual
		}
	}
}
