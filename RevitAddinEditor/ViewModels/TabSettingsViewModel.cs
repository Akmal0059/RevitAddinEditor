using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CustomRevitControls;
using RevitAddinEditor.Commands;

namespace RevitAddinEditor.ViewModels
{
    public class TabSettingsViewModel : ViewModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public ICommand CloseCommand { get; set; }


        public TabSettingsViewModel()
        {
            CloseCommand = new CloseCommand(this);
        }

    }
}
