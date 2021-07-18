using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Roslyn
{
	public interface IRuleAPI
	{
		void Boo();
	}

	public class Program
	{
        public const string code = "System.TimeSpan sinceMidnight = System.DateTime.Now - System.DateTime.Today; var seconds = sinceMidnight.TotalSeconds;";





		public static async Task Main(string[] args)
		{

			Script script = CSharpScript.Create(code, ScriptOptions.Default);
			var x = script.Compile();
			var result = await script.RunAsync();



			Console.WriteLine($"Seconds in a week: {result.GetVariable("seconds").Value}");
		}
	}
}
