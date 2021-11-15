using CustomRevitControls;
using CustomRevitControls.Interfaces;
using RevitAddinBase.RevitContainers;
using RevitAddinEditor.Commands.EditItemsCommands;
using RevitAddinEditor.Models;
using RevitAddinEditor.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Linq;
using RevitAddinEditor.Views;
using System.IO;
using System.Resources;
using System.Collections;
using System.Xml.Serialization;

namespace RevitAddinEditor.Commands
{
    public class ImportSettingsCommand : CommandBase
    {
        private EditorViewModel viewModel;
        private Dictionary<string, object> resDict { get; set; }

        public ImportSettingsCommand(EditorViewModel vm)
        {
            viewModel = vm;
            resDict = new Dictionary<string, object>();
        }

        public override void Execute(object parameter)
        {
            resDict.Clear();
            ImportUI importUI = new ImportUI();
            importUI.ShowDialog();
            if (importUI.DialogResult == true)
            {
                // string resources
                using (FileStream fs = new FileStream(importUI.TB_STRRESFilePath.Text, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    ResXResourceReader rr = new ResXResourceReader(fs);
                    foreach (DictionaryEntry item in rr)
                    {
                        resDict.Add((string)item.Key, item.Value);
                    }
                }


                // media resources
                using (FileStream fs = new FileStream(importUI.TB_MEDIARESFilePath.Text, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    ResXResourceReader rr = new ResXResourceReader(fs);
                    foreach (DictionaryEntry item in rr)
                    {
                        resDict.Add((string)item.Key, item.Value);
                    }
                }

                var ribbonTabs = Deserialize(importUI.TB_XMLFilePath.Text);
                List<RevitTab> revitTabs = new List<RevitTab>();
                foreach (var t in ribbonTabs)
                {
                    RevitTab revitTab = new RevitTab(t, resDict);

                    foreach (var panel in revitTab.Panels)
                    {
                        foreach (var control in panel.Controls)
                        {
                            SetPropeties(control);
                        }
                    }
                    revitTabs.Add(revitTab);
                }
                viewModel.Tabs = new ObservableCollection<RevitTab>(revitTabs);

            }
        }
        void SetPropeties(RevitControl control)
        {
            control.SetProperties(command: new EditItemsCommand(control),
                                  commands: new List<string>() { "1", "2", "3" });
            if (control.HasElements)
                foreach (var elem in control.Items)
                    SetPropeties(elem);

            if (control is StackButton)
            {
                foreach (var item in control.Items)
                    if (item is IStackItem)
                        (item as IStackItem).CalculateWidth();
            }

            if (control is ISplitItem splitItem)
            {
                if (splitItem.SelectedIndex == null)
                    (((RevitControl)splitItem).DataContext as ControlsContext).CurrentItem = ((RevitControl)splitItem).Items?.FirstOrDefault();
                else
                    (((RevitControl)splitItem).DataContext as ControlsContext).CurrentItem = ((RevitControl)splitItem).Items[splitItem.SelectedIndex.Value];
            }

        }

        RibbonTab[] Deserialize(string path)
        {
            RibbonTab[] tabs = null;
            XmlSerializer formatter = new XmlSerializer(typeof(RibbonTab[]));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                tabs = (RibbonTab[])formatter.Deserialize(fs);
            }
            return tabs;
        }
    }
}
