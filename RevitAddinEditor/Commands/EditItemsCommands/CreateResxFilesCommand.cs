using RevitAddinEditor.ViewModels;
using System;
using System.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.CodeDom.Compiler;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using CustomRevitControls;

namespace RevitAddinEditor.Commands.EditItemsCommands
{
    public class CreateResxFilesCommand : CommandBase
    {
        private EditorViewModel viewModel { get; }

        public CreateResxFilesCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ValidateNames = false;
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = true;
            ofd.FileName = "Выберите папку";
            ofd.ShowDialog();
            string outputFolder = Path.GetDirectoryName(ofd.FileName);

            if ((string)parameter == "img")
                SaveMediaResources($@"{outputFolder}\InpadPlugins.Images.resources");
            else
            {
                if (!Directory.Exists($@"{outputFolder}\{(string)parameter}"))
                    Directory.CreateDirectory($@"{outputFolder}\{(string)parameter}");

                SaveStringResources($@"{outputFolder}\{(string)parameter}\InpadPlugins.resources");
            }
        }

        private void SaveStringResources(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ResourceWriter rw = new ResourceWriter(fs);
                foreach (var pnl in viewModel.Panels)
                {
                    foreach (var rItem in pnl.Controls)
                        rItem.AddStringResources(rw);
                }
                rw.Generate();
            }
        }
        private void SaveMediaResources(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ResourceWriter rw = new ResourceWriter(fs);
                foreach (var pnl in viewModel.Panels)
                {
                    foreach (var rItem in pnl.Controls)
                        rItem.AddMediaResources(rw);
                }
                rw.Generate();
            }
        }
    }
}
