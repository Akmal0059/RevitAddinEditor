using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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
                Assembly assembly = Assembly.LoadFrom(openFileDialog.FileName);
                try
                {
                    var types = assembly.GetTypes();
                    viewModel.RevitItems = types.Where(x => x != null).Select(x => x.Name).ToList();
                }
                catch (ReflectionTypeLoadException e)
                {
                    var types = e.Types;
                    viewModel.RevitItems = types.Where(x=> x != null).Select(x => x?.Name).ToList();
                }
            }

        }
    }
}
