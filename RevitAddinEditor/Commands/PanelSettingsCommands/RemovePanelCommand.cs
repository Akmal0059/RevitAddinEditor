using RevitAddinEditor.Models;
using RevitAddinEditor.ViewModels;
using RevitAddinEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands
{
    public class RemovePanelCommand : CommandBase
    {
        EditorViewModel viewModel;

        public RemovePanelCommand(EditorViewModel vm) => viewModel = vm;

        public override bool CanExecute(object parameter) => viewModel.SelectedPanel != null;

        public override void Execute(object parameter)
        {
            viewModel.SelectedTab.Panels.Remove(viewModel.SelectedPanel);
        }
    }
}
