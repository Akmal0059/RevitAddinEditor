using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinEditor.Commands
{
    public class TestCommand : CommandBase
    {
        private ViewModels.EditorViewModel viewModel;

        public TestCommand(ViewModels.EditorViewModel vm) => viewModel = vm;
        public override void Execute(object parameter)
        {
            //System.Windows.Forms.MessageBox.Show(viewModel.Items.Count.ToString());
        }
    }
}
