using CustomRevitControls;
using RevitAddinEditor.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitAddinEditor.ViewModels
{
    public class SlideOutViewModel:ViewModelBase
    {
        RevitPanel panel;
        ObservableCollection<RevitControl> slideOuts;

        public RevitPanel SelectedPanel
        {
            get => panel;
            set
            {
                panel = value;
                OnPropertyChanged();
            }
        }
        public ICommand CloseCommand { get; }
        public ObservableCollection<RevitControl> SlideOuts
        {
            get => slideOuts;
            set
            {
                slideOuts = value;
                OnPropertyChanged();
            }
        }

        public SlideOutViewModel()
        {
            CloseCommand = new CloseCommand(this);
            slideOuts = new ObservableCollection<RevitControl>();
        }
    }
}
