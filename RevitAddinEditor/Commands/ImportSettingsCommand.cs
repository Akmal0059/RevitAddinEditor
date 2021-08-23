using CustomRevitControls;
using RevitAddinEditor.Models;
using RevitAddinEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RevitAddinEditor.Commands
{
    public class ImportSettingsCommand : CommandBase
    {
        private EditorViewModel viewModel;

        public ImportSettingsCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(List<RevitControl>));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(@"C:\Users\user110\Documents\test\Editor\controls.xml", FileMode.OpenOrCreate))
            {
                viewModel.Controls = new ObservableCollection<RevitControl>(((RevitPanel)formatter.Deserialize(fs)).Items);
            }
        }
    }
}
