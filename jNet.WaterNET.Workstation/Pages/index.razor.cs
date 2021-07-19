using jNet.Client.Code;
using jNet.Client.UI;
using jNet.Shared.Code;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.WaterNET.Workstation.Pages
{

	public class DefinitionTreeNode: TreeNode
	{
        public DefinitionTreeNode(string text, Func<IEnumerable<DefinitionTreeNode>> func)
        {			
			Children = func();
            Text = text;
        }
	}


	public partial class Index
	{
		private Definition? SelectedDefinition { get; set; } = default;

		public async Task<IEnumerable<TreeNode>> Nodes()
		{
			if (Store is not null)
            {
				var definitions = await Store.Get<Definition>(p => p.ParentKey is null);
				return definitions.Select(p => new DefinitionTreeNode(p.Name, () => Children(p.Key).Result) { OnSelected  = () => OnSelected(p) });
			}
			return Enumerable.Empty<TreeNode>();


			async Task<IEnumerable<DefinitionTreeNode>> Children(Guid parentKey)
            {
				 var definitions = await Store.Get<Definition>(p => p.ParentKey == parentKey);

				var kids= definitions.Select(p => new DefinitionTreeNode(p.Name, () => Children(p.Key).Result) { OnSelected = () => OnSelected(p) });

				return kids;
			}

			void OnSelected(Definition definition)
			{
				SelectedDefinition = definition;
				StateHasChanged();
			}


		}

		[Inject] Store? Store { get; set; }

		Setting settings = new() { Name = nameof(Index) };
		protected async override Task OnInitializedAsync()
		{
			if (Store is not null)
			{
				settings = (await Store.Get<Setting>(q=>q.Name== nameof(Index))).FirstOrDefault() ?? settings;
				Store.Set(settings);
			}
			await base.OnInitializedAsync();
		}

		Task Save()
		{
			if (Store is not null)
			{
				return Store.Save();
			}
			return Task.CompletedTask;
		}
	}
}
