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
    internal class EditPanelCommand : CommandBase
    {
        EditorViewModel viewModel { get; }

        public EditPanelCommand(EditorViewModel vm) => viewModel = vm;
        
        public override bool CanExecute(object parameter) => viewModel.SelectedPanel != null;

        public override void Execute(object parameter)
        {
            RevitPanel panel = viewModel.SelectedPanel;
            var psViewModel = new PanelSettingsViewModel();
            psViewModel.Name = panel.Name;
            psViewModel.Id = panel.Id;
            psViewModel.Text = panel.Text;

            PanelSettingsUI ui = new PanelSettingsUI();
            ui.DataContext = psViewModel;
            ui.ShowDialog();

            if (psViewModel.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                panel.Id = psViewModel.Id;
                panel.Name = psViewModel.Name;
                panel.Text = psViewModel.Text;
            }
        }
    }

}
