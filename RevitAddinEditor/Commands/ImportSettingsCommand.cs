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

                var tabs = RibbonTab.Deserialize(importUI.TB_XMLFilePath.Text);
                List<RevitPanel> panels = new List<RevitPanel>();
                foreach (var p in tabs.Panels)
                {
                    RevitPanel panel = new RevitPanel();
                    panel.Name = p.Name;
                    panel.Id = p.Id;
                    panel.Text = p.Text;
                    foreach (var c in p.Items)
                    {
                        var revitControl = RevitControl.GetRevitControl(c, resDict);

                        SetPropeties(revitControl);
                        panel.Controls.Add(revitControl);
                    }
                    panels.Add(panel);
                }
                viewModel.Panels = new ObservableCollection<RevitPanel>(panels);

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
    }
}
