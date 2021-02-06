using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace jNet.Blazor.Parts
{
	public abstract class MyInputBase<T> : InputBase<T>
	{
		[Parameter] public string? Id { get; set; }
		[Parameter] public string? Label { get; set; }
		[Parameter] public string? Name { get; set; }
		[Parameter] public string? Placeholder { get; set; }
		[Parameter] public string? Title { get; set; }
		[Parameter] public bool? Required { get; set; }
		[Parameter] public bool? ReadOnly { get; set; }
		[Parameter] public bool? Disabled { get; set; }

		protected Dictionary<string, object> Attributes { get; } = new Dictionary<string, object>();

		private static ConcurrentDictionary<string, Dictionary<Type, Attribute>> cache = new ConcurrentDictionary<string, Dictionary<Type, Attribute>>();

		public string FullName { get; private set; } = "";

		protected override void OnParametersSet()
		{
			base.OnParametersSet();
			FullName = $"{EditContext.Model.GetType().FullName}.{FieldIdentifier.FieldName}";

			if (!cache.ContainsKey(FullName))
			{
				var me = ValueExpression!.Body as MemberExpression;
				var attr = me!.Member.GetCustomAttributes();
				var dict = attr.ToDictionary(q => q.GetType());
				cache[FullName] = dict;
			}

			Display = Get<DisplayAttribute>();
			Required ??= Get<RequiredAttribute>() != null;
			Disabled ??= Get<KeyAttribute>() != null;
			ReadOnly ??= !(Get<EditableAttribute>()?.AllowEdit ?? true);

			Title ??= Display?.Description;
			Label ??= Display?.Name ?? FieldIdentifier.FieldName;
			Name ??= FieldIdentifier.FieldName;
			DisplayName = Label;
			Placeholder ??= Display?.Prompt;
			Id ??= $"id{GetHashCode()}";

			if (Id != null) { Attributes[nameof(Id).ToLowerInvariant()] = Id; }
			if (Name != null) { Attributes[nameof(Name).ToLowerInvariant()] = Name; }
			if (Required.Value) { Attributes[nameof(Required).ToLowerInvariant()] = Required; }
			if (Disabled.Value) { Attributes[nameof(Disabled).ToLowerInvariant()] = Disabled; }
			if (ReadOnly.Value) { Attributes[nameof(Required).ToLowerInvariant()] = ReadOnly; }
			if (Placeholder != null) { Attributes[nameof(Placeholder).ToLowerInvariant()] = Placeholder; }
			if (Title != null) { Attributes[nameof(Title).ToLowerInvariant()] = Title; }


			if (AdditionalAttributes != null)
			{
				foreach (var kv in AdditionalAttributes)
				{
					Attributes[kv.Key] = kv.Value;
				}
			}
		}

		public A? Get<A>()
			where A : Attribute
		{
			var dict = cache[FullName];
			if (dict.TryGetValue(typeof(A), out var attr))
			{
				return (A)attr;
			}
			return null;
		}

		public DisplayAttribute? Display { get; private set; }

		// placeholder="@Placeholder" title="@Title" readonly="@ReadOnly" required="@Required" disabled="@IsAKey"
	}

	public enum IntergerType : byte
	{
		Number,
		Range,
	}
}
