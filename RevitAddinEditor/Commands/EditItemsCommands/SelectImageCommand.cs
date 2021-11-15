using CustomRevitControls;
using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RevitAddinEditor.Commands.EditItemsCommands
{
    public class SelectImageCommand : CommandBase
    {
        private PanelViewModel viewModel;
        public SelectImageCommand(PanelViewModel vm) => viewModel = vm;
        public override void Execute(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                viewModel.SelectedControl.Icon = GetBitmapSource(dialog.FileName);
            }
        }
    }
}
