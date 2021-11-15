using CustomRevitControls;
using RevitAddinEditor.ViewModels;
using RevitAddinEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands.TabSettingsCommands
{
    public class EditTabCommand : CommandBase
    {
        EditorViewModel viewModel;

        public EditTabCommand(EditorViewModel vm) => viewModel = vm;

        public override bool CanExecute(object parameter) => viewModel.SelectedTab != null;

        public override void Execute(object parameter)
        {
            RevitTab tab = viewModel.SelectedTab;
            var tsViewModel = new TabSettingsViewModel();
            tsViewModel.Name = tab.Name;
            tsViewModel.Id = tab.Id;
            tsViewModel.Title = tab.Title;

            TabSettingsUI ui = new TabSettingsUI();
            ui.DataContext = tsViewModel;
            ui.ShowDialog();

            if (tsViewModel.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                tab.Id = tsViewModel.Id;
                tab.Name = tsViewModel.Name;
                tab.Title = tsViewModel.Title;
            }
        }
    }
}
