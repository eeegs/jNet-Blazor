using System;
using System.Windows.Input;

namespace jNet.Accounts.Components
{
	public class Command : ICommand
	{
		readonly Action<object?> action;
		readonly Func<object?, Action, bool> predicate = (_, _) => true;
		bool isEnabled = true;

		public static Command Noop = new Command(_ => { });

		public event EventHandler? CanExecuteChanged;

		public Command(Action<object?> action) : base()
		{
			this.action = action;
		}

		public Command(Action<object?> action, Func<object?, Action, bool> predicate)
		{
			this.action = action;
			this.predicate = predicate;
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

		public void RaiseStateChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
		public bool CanExecute(object? param = default) => isEnabled && predicate(param, RaiseStateChanged);
		public void Execute(object? param = default)
		{
			if (CanExecute(param))
			{
				action(param);
			}
		}
	}
}
