using CustomRevitControls;
using RevitAddinEditor.ViewModels;
using RevitAddinEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands
{
    public class EditItemsCommand : CommandBase
    {
        private PanelViewModel viewModel;

        public EditItemsCommand(PanelViewModel vm) => viewModel = vm;

        public override bool CanExecute(object parameter)
        {
            if (viewModel.SelectedControl != null)
                return ((RevitControl)viewModel.SelectedControl).HasElements;
            else return false;
        }

        public override void Execute(object parameter)
        {
            AddNewControlUI ui = new AddNewControlUI(((RevitControl)viewModel.SelectedControl).Items);
            ui.ShowDialog();
        }
    }
}
