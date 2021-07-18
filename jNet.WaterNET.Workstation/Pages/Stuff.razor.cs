using jNet.Client.Code;
using jNet.Shared.Code;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jNet.WaterNET.Workstation.Pages
{

	public partial class Stuff
	{
		[Inject] Store? store { get; set; }

		Task<Instance?> Instance = Task.FromResult<Instance?>(default);

		private bool Flip;
		private bool Direction;
		private bool CanClose;

		double[] Splits = { 50, 100, 200 };

		private Setting settings = new();

		protected async override Task OnParametersSetAsync()
		{
			if (store is not null)
			{
				//makedata(store);
				Instance = store.Get<Instance>(1);
				settings = await store.Get<Setting>(new Guid("fc47348b-5403-4e8f-94de-6674138b6354")) ?? settings;
			}
			await base.OnParametersSetAsync();
		}

		static void makedata(Store store)
		{
			var f1 = new Field { Name = "Model number", Type = FieldType.Text, Required = false };
			var f2 = new Field { Name = "Model name", Type = FieldType.Text, Required = true };
			var f3 = new Field { Name = "Brand", Type = FieldType.Text, Required = true };
			var f4 = new Field { Name = "Serial#", Type = FieldType.Text, Required = true };
			var f5 = new Field { Name = "Age", Type = FieldType.Integer, Required = false };

			var p1 = new Definition
			{
				Name = "Pump",
				DefinitionFields = new() { f3 },
				InstanceFields = new() { f4 },
			};

			var p2 = new Definition(p1)
			{
				Name = "Davie",
				DefinitionFields = new() { f1, f2 },
				InstanceFields = new() { f5 },
			};

			var e1 = new Instance(p1);
			var e2 = new Instance(p2);
			var n1 = e1 + e2;
			var n2 = -n1;

			store.Set(n1, n2);
			store.Set(p1, p2);
			store.Set(e1, e2);
			store.Save();
		}

		Setting.Split this[string key] { get => settings[key]!; set => settings[key] = value; }

		Task Save()
		{
			if (store is not null)
			{
				return store.Save();
			}
			return Task.CompletedTask;
		}
	}
}


