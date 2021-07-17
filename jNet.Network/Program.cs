using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using jNet.Numerics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace jNet.Network
{
	class Program
	{
		static void Main(string[] args)
		{
			var path = @"C:\Users\sscot\temp\storage.json";

			var store = Storage.Load(path);
			//var store = new Storage();

			//var def = store.CreateType("Pump");
			//def["Fred"] = "Tom";
			//def.Ports.Add(new Port { Name = "Port A" });
			//var sub = store.CreateType(def, "Davie");
			//var subsub = store.CreateType(sub, "BlueSteel");
			//subsub["Tom"] = "Jane";

			//var s = subsub.Path;

			//var n1 = store.CreateEquipment(sub);
			//var n2 = store.CreateEquipment(subsub, "123131-234.234");
			//var x = store.CreateConnector(n1, n2, true);
			store.Save(path);
		}

		static void Main3(string[] args)
		{
			var v = 12 * Unit.Volt;
			var i = 100e-3 * Unit.Ampere;
			var w = 1 * Unit.Sievert;
			var p = v * i * w;
			var q = v.Unit.kg;

			var e = p.Unit == Unit.Watt;

			Console.WriteLine($"Hello World!: {p} = {v} * {i}, {q}, {e}, {p.asUnit()}");
		}
	}

	[JsonObject(IsReference = false)]
	public class Storage : IId
	{
		public int Id { get; set; }

		public List<EquipmentType> EquipmentTypes { get; } = new();
		public List<Equipment> Equipment { get; } = new();
		public List<Connector> Connectors { get; } = new();

		public int NextKey() => ++Id;

		public Storage()
		{
		}

		public void Save(string path)
		{
			var options = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
			var txt = JsonConvert.SerializeObject(this, Formatting.Indented, options);
			System.IO.File.WriteAllText(path, txt);
		}

		public static Storage Load(string path)
		{
			var options = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
			var txt = System.IO.File.ReadAllText(path);
			var result = JsonConvert.DeserializeObject<Storage>(txt, options);

			foreach (var c in result!.Connectors)
			{
				if (c.Inverse != null)
				{
					c.Inverse.Inverse = c;
				}
			}
			return result!;
		}

		private T KeyIt<T>(T value) where T : IId
		{
			value.Id = ++Id;
			return value;
		}

		public EquipmentType CreateType(string name)
		{
			var res = KeyIt(new EquipmentType(name));
			//EquipmentTypes[res.Key] = res;
			EquipmentTypes.Add(res);
			return res;
		}
		public EquipmentType CreateType(EquipmentType parentType, string name)
		{
			var res = KeyIt(new EquipmentType(name, parentType));
			//EquipmentTypes[res.Key] = res;
			EquipmentTypes.Add(res);
			return res;
		}

		public Equipment CreateEquipment(EquipmentType equipmentType, string name = "")
		{
			var res = KeyIt(string.IsNullOrWhiteSpace(name) ? new Equipment(equipmentType) : new Equipment(equipmentType, name));
			//Equipment[res.Key]=res;
			Equipment.Add(res); ;
			return res;
		}
		public Connector CreateConnector(Equipment a, Equipment b, bool bidirectional = false)
		{
			var c1 = KeyIt(new Connector(a, b));
			Connectors.Add(c1);
			if (bidirectional)
			{
				var c2 = KeyIt(new Connector(b, a));
				Connectors.Add(c2);
				c1.Inverse = c2;
				c2.Inverse = c1;
			}
			return c1;
		}
	}
}
