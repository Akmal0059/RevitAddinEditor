using RevitAddinEditor.ViewModels;
using RevitAddinEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands
{
    public class OpenItemsEditorCommand : CommandBase
    {
        private EditorViewModel viewModel;

        public OpenItemsEditorCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            AddNewControlUI ui = new AddNewControlUI(viewModel.Controls);
            ui.ShowDialog();

            viewModel.Controls = (ui.DataContext as PanelViewModel).Controls;
        }
    }
}
