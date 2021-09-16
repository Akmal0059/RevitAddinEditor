using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RevitAddinEditor.Commands
{
    public class CloseCommand : CommandBase
    {
        private ViewModelBase viewModel;

        public CloseCommand(ViewModelBase vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            Button btn = (Button)(parameter as object[])[0];
            Window win = (Window)(parameter as object[])[1];
            int tag = -1;
            if (Int32.TryParse((string)btn.Tag, out tag))
            {
                if (tag == 1)
                {
                    viewModel.DialogResult = System.Windows.Forms.DialogResult.OK;
                    win.Close();
                    return;
                }
            }
            viewModel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            win.Close();
        }
    }
}
