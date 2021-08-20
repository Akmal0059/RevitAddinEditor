using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RevitAddinEditor.Commands
{
    public class MoveCommand : CommandBase
    {
        private PanelViewModel viewModel;

        public MoveCommand(PanelViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            var dir = Int32.Parse(parameter.ToString());
            var control = viewModel.SelectedControl;
            int index = viewModel.Controls.IndexOf(viewModel.SelectedControl);
            if ((index == 0 && dir == -1) ||
                (index == viewModel.Controls.Count -1 && dir == 1))
                return;

            viewModel.Controls.Remove(viewModel.SelectedControl);
            viewModel.Controls.Insert(index + dir, control);
            viewModel.SelectedControl = control;
        }
    }
}
