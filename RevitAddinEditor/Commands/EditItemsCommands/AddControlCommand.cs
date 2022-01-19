using CustomRevitControls;
using RevitAddinEditor.Models;
using RevitAddinEditor.ViewModels;
using RevitAddinEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RevitAddinEditor.Commands.EditItemsCommands
{
    public class AddControlCommand : CommandBase
    {
        private PanelViewModel viewModel;

        public AddControlCommand(PanelViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            RevitControl control = null;

            switch (viewModel.SelectedControlType.Type)
            {
                case ControlType.Separator:
                    control = new CustomRevitControls.Separator();
                    control.IsEnabled = false;
                    break;
                case ControlType.Regular:
                    control = new RegularButton();
                    break;
                case ControlType.Pulldown:
                    control = new PulldownButton();
                    break;
                case ControlType.SplitButton:
                    control = new SplitItem();
                    break;
                case ControlType.StackButton:
                    control = new StackButton();
                    break;
                case ControlType.StackedPulldown:
                    control = new StackedPulldown();
                    break;
                case ControlType.StackedSplitItem:
                    control = new StackedSplitItem();
                    break;
                case ControlType.StackedRegButton:
                    control = new StackedRegularButton();
                    break;
                case ControlType.TextBox:
                    control = new TextBoxItem();
                    break;
                case ControlType.Textblock:
                    control = new Textblock();
                    control.IsEnabled = false;
                    break;
                case ControlType.Checkbox:
                    control = new Checkbox();
                    break;
                case ControlType.Combobox:
                    control = new Combobox();
                    break;
                case ControlType.RadioGroup:
                    control = new RadioGroup();
                    break;
                case ControlType.ToggleButton:
                    control = new ToggleButton();
                    break;
                default:
                    break;
            }

            if (control != null)
            {
                control.SetProperties(command: new EditItemsCommand(control));
                viewModel.Controls.Add(control);
                viewModel.SelectedControl = control;
            }
        }
    }
}
