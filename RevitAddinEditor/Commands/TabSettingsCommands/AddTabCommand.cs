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
    public class AddTabCommand : CommandBase
    {
        EditorViewModel viewModel;

        public AddTabCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            RevitTab tab = new RevitTab();
            TabSettingsUI ui = new TabSettingsUI();
            ui.ShowDialog();
            var tsViewModel = ui.DataContext as TabSettingsViewModel;
            if (tsViewModel.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                tab.Id = tsViewModel.Id;
                tab.Name = tsViewModel.Name;
                tab.Title = tsViewModel.Title;
                viewModel.Tabs.Add(tab);
            }
            viewModel.SelectedTab = tab;
        }
    }
}
