using RevitAddinEditor.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RevitAddinEditor.ViewModels
{
    public class PanelViewModel:ViewModelBase
    {
        string name;
        ObservableCollection<Control> controls;
        Control selectedControl;
        int ctype;

        public string Name 
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Control> Controls 
        {
            get => controls;
            set
            {
                controls = value;
                OnPropertyChanged();
            }
        }
        public Control SelectedControl 
        {
            get => selectedControl;
            set
            {
                selectedControl = value;
                OnPropertyChanged();
            }
        }
        public ControlType ControlType 
        {
            get => (ControlType)ctype;
            set
            {
                ctype = (int)value;
                OnPropertyChanged();
            }
        }
        public ICommand AddControlCommand { get; set; }
        public ICommand DeleteControlCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }
        public ICommand MoveCommand {  get; set; }

        public PanelViewModel()
        {
            Controls = new ObservableCollection<Control>();
            AddControlCommand = new AddControlCommand(this);
            SelectImageCommand = new SelectImageCommand(this);
            DeleteControlCommand = new DeleteControlCommand(this);
            MoveCommand = new MoveCommand(this);
        }
    }

    public enum ControlType
    {
        Regular = 0,
        Pulldown = 1,
        SplitButton = 2,
        StackButton = 3,
        StackedPulldown = 4,
        StackedSplitItem = 5,
        StackedItem = 6,
        TextBox = 7
    }
}
