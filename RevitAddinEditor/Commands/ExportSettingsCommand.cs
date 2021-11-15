using CustomRevitControls;
using RevitAddinBase;
using RevitAddinBase.RevitContainers;
using RevitAddinBase.RevitControls;
using RevitAddinEditor.Models;
using RevitAddinEditor.ViewModels;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RevitAddinEditor.Commands
{
    public class ExportSettingsCommand : CommandBase
    {
        private EditorViewModel viewModel;

        public ExportSettingsCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {

            List<RibbonTab> tabs = new List<RibbonTab>();
            foreach (var tab in viewModel.Tabs)
            {
                tabs.Add(tab.GetRibbonTab());
            }

            Serialize(tabs);
        }

        void Serialize(List<RibbonTab> tabs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<RibbonTab>));

                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, tabs);
                }
            }
        }
    }
}
