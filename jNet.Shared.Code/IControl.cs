using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Shared.Code
{
	public interface IControl
	{
		bool Enabled(object? param = default);
		string Label { get; set; }

		event EventHandler? StateChanged;
	}

	public interface ICommand : IControl
	{
		void Execute(object? param = default);
	}

	public interface IField<V> : IControl
	{
		V? Value { get; set; }
	}

	public abstract class ControlBase
	{
		readonly Predicate<object?> predicate;
		bool isEnabled = true;
		string label;

		public ControlBase(string label)
		{
			this.label = label;
			predicate = _ => isEnabled;
		}

		public ControlBase(string label, Predicate<object?> predicate)
		{
			this.label = label;
			this.predicate = predicate;
		}

		public string Label
		{
			get => label;
			set
			{
				if (label != value)
				{
					label = value;
					RaiseStateChanged();
				}
			}
		}
		public bool IsEnabled
		{
			get => isEnabled;
			set
			{
				if (isEnabled != value)
				{
					isEnabled = value;
					RaiseStateChanged();
				}
			}
		}

		public event EventHandler? StateChanged;

		public bool Enabled(object? param = default) => isEnabled && predicate(param);
		public void RaiseStateChanged() => StateChanged?.Invoke(this, new EventArgs());
	}

	public class Command : ControlBase, ICommand
	{
		readonly Action<object?> action;

		public Command(string label, Action<object?> action) : base(label)
		{
			this.action = action;
		}

		public Command(string label, Action<object?> action, Predicate<object?> predicate) : base(label, predicate)
		{
			this.action = action;
		}

		public void Execute(object? param = default) => action.Invoke(param);
	}

	public class Field<V> : ControlBase, IField<V>
	{
		V? value = default;

		public Field(string label) : base(label)
		{
		}

		public Field(string label, Predicate<object?> predicate) : base(label, predicate)
		{
		}

		public V? Value
		{
			get => value;
			set
			{
				if (!EqualityComparer<V>.Default.Equals(this.value, value))
				{
					this.value = value;
					RaiseStateChanged();
				}
			}
		}
	}
}
