using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CustomRevitControls.Interfaces;
using RevitAddinBase.RevitCommands;
using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Policy;
using System.Text;
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
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Assembly (*.dll)|*.dll";
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            var dll = File.ReadAllBytes(@"C:\Users\user110\source\repos\testAddin\testAddin\bin\Debug via Revit Add-In Manager\RevitAddin.dll");
            var dll2 = File.ReadAllBytes(@"C:\Users\user110\source\repos\RevitPlugins (prod)\InpadPlugins\bin\Debug2\INPDPlugins.dll");
            var pdb = File.ReadAllBytes(@"C:\Users\user110\source\repos\testAddin\testAddin\bin\Debug via Revit Add-In Manager\RevitAddin.pdb");
            Evidence evidence = new Evidence();
            //Assembly assembly = Assembly.Load(dll, pdb, System.Security.SecurityContextSource.CurrentAssembly);

            Assembly assembly = Assembly.LoadFrom(@"C:\Users\user110\source\repos\BoxChecker\BoxChecker\bin\Debug via Revit Add-In Manager\BoxChecker.dll");
            try
            {
                //var inst1 = assembly.CreateInstance("testAddin.ExtCommand");
                //var inst2 = assembly.CreateInstance("testAddin.Command");

                var types = assembly.GetTypes();

                viewModel.RevitItems = types.Where(x => IsRevitCommand(x)).Select(x => x.Name).ToList();
            }
            catch (ReflectionTypeLoadException e)
            {
                var types = e.Types.OrderBy(x=>x?.Name).ToList();

                var commands = types.Where(x => IsRevitCommand(x)).Select(x => x?.Name).ToList();
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in e.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                System.Windows.Forms.MessageBox.Show(errorMessage);
                //Display or log the error based on your application.
            }
            //}
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

                result = type.IsSubclassOf(typeof(SingletonCommand));
            }
            catch { }

            return result;
        }
    }
}
