using Mono.Cecil;
using RevitAddinEditor.ViewModels;
using System;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace RevitAddinEditor.Commands
{
    public class SetAssemblyCommand : CommandBase
    {
        private EditorViewModel viewModel;
        public SetAssemblyCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Assembly (*.dll)|*.dll";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(openFileDialog.FileName);
                var types = assemblyDefinition.MainModule.Types.ToList();
                viewModel.SingleCommands = types.Where(x => x.BaseType?.FullName == "RevitAddinBase.RevitCommands.SingletonCommand").Select(x=>x.FullName).ToList();
                viewModel.ComboBoxes = types.Where(x => x.BaseType?.FullName == "RevitAddinBase.RevitControls.ComboBox").Select(x => x.FullName).ToList();
                viewModel.AssemblyPath = openFileDialog.FileName;

            }
        }

        public static BitmapSource GetResourceImage(Type cmd_type, string key)
        {
            if (cmd_type == (Type)null)
                throw new ArgumentNullException(nameof(cmd_type));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException(nameof(cmd_type));

            ResourceManager resourceManager1 = new ResourceManager(cmd_type);

            Bitmap bitmap = resourceManager1.GetObject(key) as Bitmap;
            resourceManager1.ReleaseAllResources();

            if (bitmap == null)
                return null;
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        bool IsRevitCommand(Type type)
        {
            bool result = false;
            try
            {
                if (type == null)
                    return false;

                result = type.IsSubclassOf(typeof(RevitAddinBase.RevitCommands.SingletonCommand));
            }
            catch { }

            return result;
        }
    }
}
