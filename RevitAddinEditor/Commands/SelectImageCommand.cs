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

namespace RevitAddinEditor.Commands
{
    public class SelectImageCommand : CommandBase
    {
        private PanelViewModel viewModel;
        public SelectImageCommand(PanelViewModel vm) => viewModel = vm;
        public override void Execute(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image (*.png)|*.png";
            if(dialog.ShowDialog() == DialogResult.OK)
{
                viewModel.SelectedControl.MainIcon = GetImageSource(dialog.FileName);
            }
        }
        ImageSource GetImageSource(string path)
        {
            var bitmap = new Bitmap(path);
            var imageSource = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                imageSource.BeginInit();
                imageSource.StreamSource = memory;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
            }
            return imageSource;
        }
    }
}
