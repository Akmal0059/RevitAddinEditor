using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands.TabSettingsCommands
{
    public class RemoveTabCommand : CommandBase
    {
        EditorViewModel viewModel;

        public RemoveTabCommand(EditorViewModel vm) => viewModel = vm;

        public override bool CanExecute(object parameter) => viewModel.SelectedTab != null;

        public override void Execute(object parameter)
        {
            viewModel.Tabs.Remove(viewModel.SelectedTab);
        }
    }
}
