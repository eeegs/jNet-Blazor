using jNet.Data;
using m = jNet.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;

namespace jNet.Command
{
	class Program
	{
		static void Main(string[] args)
		{
			long x = 10 * 10000;
			double z = x / 10000.0;
			Console.WriteLine($"{z:c}");



			
		}

	}
}
