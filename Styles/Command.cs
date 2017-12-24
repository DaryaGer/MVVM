using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Styles
{
	internal class Command : ICommand
	{
		public Action<string> Function { get; set; }
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (parameter == null || !(parameter is string)) return;
			Function((string)parameter);
		}
	}
}
