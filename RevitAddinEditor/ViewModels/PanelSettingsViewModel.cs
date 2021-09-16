using RevitAddinEditor.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace RevitAddinEditor.ViewModels
{
    public class PanelSettingsViewModel : ViewModelBase
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public ICommand CloseCommand { get; }

        public PanelSettingsViewModel()
        {
            CloseCommand = new CloseCommand(this);
        }
    }
}
