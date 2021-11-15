using CustomRevitControls;
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
    public class AddPanelCommand : CommandBase
    {
        EditorViewModel viewModel;

        public AddPanelCommand(EditorViewModel vm) => viewModel = vm;

        public override bool CanExecute(object parameter) => viewModel.SelectedTab != null;

        public override void Execute(object parameter)
        {
            RevitPanel panel = new RevitPanel();
            PanelSettingsUI ui = new PanelSettingsUI();
            ui.ShowDialog();
            var psViewModel = ui.DataContext as PanelSettingsViewModel;
            if (psViewModel.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                panel.Id = psViewModel.Id;
                panel.Name = psViewModel.Name;
                panel.Text = psViewModel.Text;
                panel.HasArrowButton = psViewModel.HasArrowButton;
                viewModel.SelectedTab.Panels.Add(panel);
            }
            viewModel.SelectedPanel = panel;
        }
    }
}
