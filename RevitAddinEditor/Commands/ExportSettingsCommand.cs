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
            RibbonTab ribbonTab = new RibbonTab("InpadPlugins");

            foreach (var p in viewModel.Panels)
            {
                var panel = new RibbonPanel();
                panel.Name = p.Name;
                panel.Id = p.Id;
                panel.Text = p.Text;
                panel.Items = new List<RibbonItemBase>();

                foreach (var item in p.Controls)
                {
                    var ribbon = item.GetRevitRibbon();
                    ribbon.Name = "name";
                    ribbon.Description = "description";
                    panel.Items.Add(ribbon);
                }
                ribbonTab.Panels.Add(panel);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ribbonTab.Serialize(saveFileDialog.FileName);
            }
        }
    }
}
