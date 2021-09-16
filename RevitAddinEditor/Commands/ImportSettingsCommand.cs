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

namespace RevitAddinEditor.Commands
{
    public class ImportSettingsCommand : CommandBase
    {
        private EditorViewModel viewModel;

        public ImportSettingsCommand(EditorViewModel vm) => viewModel = vm;

        public override void Execute(object parameter)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml Files |*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var tabs = RibbonTab.Deserialize(dialog.FileName);
                List<RevitPanel> panels = new List<RevitPanel>();
                foreach (var p in tabs.Panels)
                {
                    RevitPanel panel = new RevitPanel();
                    panel.Name = p.Name;
                    panel.Id = p.Id;
                    panel.Text = p.Text;
                    foreach (var c in p.Items)
                    {
                        var revitControl = RevitControl.GetRevitControl(c);
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
