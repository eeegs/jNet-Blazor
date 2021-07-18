using jNet.Autoform;
using jNet.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;

namespace jNet.Blazor.Client
{

	[Authorize]
	public class BaseRazor : OwningComponentBase
	{
		[Inject]
		protected HttpClient Http { get; private set; }

		[Inject]
		protected NavigationManager NavigationManager { get; private set; }

		[Inject]
		protected IJSRuntime JSRuntime { get; private set; }
	}

	public abstract class BaseRazor<T, K> : BaseRazor
		where T : class, IHaveId<K>, new()
	{
		private bool disposedValue;
		protected EntityCache<T> eModel { get; } = new EntityCache<T>();

		public BaseRazor()
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}
	}

	public enum Colour
	{
		[Description("Hampton's Blue")] HamptonsBlue = 11510655,   // #7fa3af
		[Description("Dragon's Eye")] DragonsEye = 6314410,   // #aa5960
		[Description("Sundance")] Sundance = 5948903,   // #e7c55a
		[Description("Gulf Stream")] GulfStream = 10323768,   // #38879d
		[Description("Bayleaf")] Bayleaf = 9547661,   // #8daf91
		[Description("Gunmetal Grey")] GunmetalGrey = 9272423,   // #677c8d
		[Description("Yacht Race")] YachtRace = 7296840,   // #48576f
		[Description("Timberline")] Timberline = 8297364,   // #949b7e
		[Description("French Green")] FrenchGreen = 11912383,   // #bfc4b5
		[Description("Hailstorm")] Hailstorm = 12695954,   // #92b9c1
		[Description("Chintz Grey")] ChintzGrey = 15000534,   // #d6e3e4
		[Description("Aniseed")] Aniseed = 4933186,   // #42464b
		[Description("Rover Stone")] RoverStone = 6250073,   // #595e5f
		[Description("Mineral")] Mineral = 7829619,   // #737877
		[Description("Dusty Mule")] DustyMule = 13026752,   // #c0c5c6
		[Description("Old Stone Wall")] OldStoneWall = 12700361,   // #c9cac1
		[Description("Squid Ink")] SquidInk = 7826781,   // #5d6d77
		[Description("Shell Grey")] ShellGrey = 13092282,   // #bac5c7
		[Description("Newport Blue")] NewportBlue = 14735814,   // #c6d9e0
		[Description("Mist")] Mist = 14933969,   // #d1dfe3
		[Description("Baby Doll")] BabyDoll = 13817589,   // #f5d6d2
		[Description("Petal Pink")] PetalPink = 14672623,   // #efe2df
		[Description("Rubble")] Rubble = 14278622,   // #dedfd9
		[Description("Volcanic Ash")] VolcanicAsh = 14278622,   // #dedfd9
		[Description("Irish Linen")] IrishLinen = 15199208,   // #e8ebe7
		[Description("Popcorn")] Popcorn = 15724519,   // #e7efef
		[Description("Wet Cement")] WetCement = 8821668,   // #a49b86
		[Description("Wattleseed")] Wattleseed = 9476505,   // #999990
		[Description("Wood Smoke")] WoodSmoke = 14342611,   // #d3d9da
		[Description("Half Wood Smoke")] HalfWoodSmoke = 14343129,   // #d9dbda
		[Description("Dark Newport Blue")] DarkNewportBlue = 11378309,   // #859ead
		[Description("Explorer Blue")] ExplorerBlue = 13548453,   // #a5bbce
	}
}
