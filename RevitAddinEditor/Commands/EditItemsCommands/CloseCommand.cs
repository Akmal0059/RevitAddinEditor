using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RevitAddinEditor.Commands.EditItemsCommands
{
    public class CloseCommand : CommandBase
    {
        private PanelViewModel viewModel;

        public CloseCommand(PanelViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            Button btn = (Button)(parameter as object[])[0];
            Window win = (Window)(parameter as object[])[1];

            if ((string)btn.Content == "OK")
            {
                viewModel.DialogResult = System.Windows.Forms.DialogResult.OK;
                win.Close();
            }
            else if ((string)btn.Content == "Cancel")
            {
                viewModel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                win.Close();
            }
        }
    }
}
