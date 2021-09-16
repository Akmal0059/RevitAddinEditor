using CustomRevitControls;
using CustomRevitControls.Interfaces;
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
    public class OpenItemsEditorCommand : CommandBase
    {
        private EditorViewModel viewModel;

        public OpenItemsEditorCommand(EditorViewModel vm) => viewModel = vm;
        public override bool CanExecute(object parameter) => viewModel.SelectedPanel != null;
        public override void Execute(object parameter)
        {
            AddNewControlUI ui = new AddNewControlUI(viewModel.SelectedPanel.Controls);
            foreach(var addindControl in (ui.DataContext as PanelViewModel).AddingControls)
            {
                if (addindControl.Type == ControlType.Regular || addindControl.Type == ControlType.StackButton ||
                    addindControl.Type == ControlType.SplitButton || addindControl.Type == ControlType.Pulldown)
                    addindControl.Visible = true;
            }
            ui.ShowDialog();
            if ((ui.DataContext as PanelViewModel).DialogResult == System.Windows.Forms.DialogResult.OK)
                viewModel.SelectedPanel.Controls = (ui.DataContext as PanelViewModel).Controls;
            ui.UpdateLayout();
            foreach(var control in (ui.DataContext as PanelViewModel).Controls)
            {
                if(control is ISplitItem splitItem)
                {
                    if (splitItem.SelectedIndex == null)
                        (((RevitControl)splitItem).DataContext as ControlsContext).CurrentItem = ((RevitControl)splitItem).Items?.FirstOrDefault();
                    else 
                        (((RevitControl)splitItem).DataContext as ControlsContext).CurrentItem = ((RevitControl)splitItem).Items[splitItem.SelectedIndex.Value];
                }
            }
        }
    }
}
