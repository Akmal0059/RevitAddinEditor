using CustomRevitControls;
using CustomRevitControls.Items;
using RevitAddinEditor.Models;
using RevitAddinEditor.ViewModels;
using RevitAddinEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands.EditItemsCommands
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
            if (viewModel.SelectedControl.Items == null)
                viewModel.SelectedControl.Items = new List<RevitControl>();
            AddNewControlUI ui = new AddNewControlUI(viewModel.SelectedControl.Items);
            foreach (var addindControl in (ui.DataContext as PanelViewModel).AddingControls)
            {
                if (addindControl.Type == ControlType.TextBox || addindControl.Type == ControlType.StackedSplitItem ||
                    addindControl.Type == ControlType.StackedPulldown || addindControl.Type == ControlType.StackedItem)
                    addindControl.Visible = true;
            }
            ui.ShowDialog();
            if ((ui.DataContext as PanelViewModel).DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                viewModel.SelectedControl.Items = (ui.DataContext as PanelViewModel).Controls.ToList();
                if(viewModel.SelectedControl is StackButton)
                {
                    foreach (IStackItem item in viewModel.SelectedControl.Items)
                        item.CalculateWidth();
                }
            }
        }
    }
}
