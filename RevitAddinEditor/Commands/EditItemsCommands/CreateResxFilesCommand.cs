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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".resx";
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.ValidateNames = false;
            //ofd.CheckFileExists = false;
            //ofd.CheckPathExists = true;
            //ofd.FileName = "Выберите папку";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((string)parameter == "img")
                    SaveMediaResources(saveFileDialog.FileName);
                else if ((string)parameter == "str")
                    SaveStringResources(saveFileDialog.FileName);
            }
        }

        private void SaveStringResources(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ResXResourceWriter rw = new ResXResourceWriter(fs);
                foreach (var pnl in viewModel.SelectedTab.Panels)
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
                ResXResourceWriter rw = new ResXResourceWriter(fs);
                foreach (var pnl in viewModel.SelectedTab.Panels)
                {
                    foreach (var rItem in pnl.Controls)
                        rItem.AddMediaResources(rw);
                }
                rw.Generate();
            }
        }
    }
}
