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

namespace RevitAddinEditor.Commands.EditItemsCommands
{
    public class EditItemsCommand : CommandBase
    {
        private EditorViewModel viewModel;
        RevitControl revitControl;

        public EditItemsCommand(RevitControl rc, EditorViewModel editorlViewModel)
        {
            revitControl = rc;
            viewModel = editorlViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            //if (viewModel.SelectedControl != null)
            //    return ((RevitControl)viewModel.SelectedControl).HasElements;
            //else return false;
            return true;
        }

        public override void Execute(object parameter)
        {
            if (revitControl.Items == null)
                revitControl.Items = new List<RevitControl>();
            AddNewControlUI ui = new AddNewControlUI(revitControl.Items);
            var vm = ui.DataContext as PanelViewModel;
            vm.EditorViewModel = viewModel;
            if (revitControl is StackedPulldown || revitControl is StackedSplitItem ||
                revitControl is PulldownButton || revitControl is SplitItem || revitControl is Combobox)
            {
                foreach (var addindControl in vm.AddingControls)
                {
                    if (addindControl.Type == ControlType.StackedRegButton)
                        addindControl.Visible = true;
                }
            }
            else if(revitControl is RadioGroup)
            {
                foreach (var addindControl in vm.AddingControls)
                {
                    if (addindControl.Type == ControlType.ToggleButton)
                        addindControl.Visible = true;
                }
            }
            else
            {
                foreach (var addindControl in vm.AddingControls)
                {
                    if (addindControl.Type == ControlType.TextBox || addindControl.Type == ControlType.StackedSplitItem ||
                        addindControl.Type == ControlType.StackedPulldown || addindControl.Type == ControlType.StackedRegButton ||
                        addindControl.Type == ControlType.Textblock || addindControl.Type == ControlType.Checkbox || addindControl.Type == ControlType.Combobox)
                        addindControl.Visible = true;
                }
            }
            
            vm.SelectedControlType = vm.AddingControls.FirstOrDefault(x => x.Visible);
            ui.ShowDialog();

            if (vm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                revitControl.Items = vm.Controls.ToList();
                if (revitControl is StackButton)
                {
                    foreach (var item in revitControl.Items)
                        if (item is IStackItem)
                            (item as IStackItem).CalculateWidth();
                }
            }
            foreach (var control in (ui.DataContext as PanelViewModel).Controls)
            {
                if (control is ISplitItem splitItem)
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
