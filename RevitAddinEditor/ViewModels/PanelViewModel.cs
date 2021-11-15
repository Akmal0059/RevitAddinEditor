using CustomRevitControls;
using RevitAddinEditor.Commands;
using RevitAddinEditor.Commands.EditItemsCommands;
using RevitAddinEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace RevitAddinEditor.ViewModels
{
    public class PanelViewModel:ViewModelBase
    {
        string name;
        ObservableCollection<RevitControl> controls;
        RevitControl selectedControl;
        AddingControl selectedControlType;
        public List<string> Test { get; set; }
        public List<AddingControl> AddingControls { get; set; }
        public AddingControl SelectedControlType 
        {
            get => selectedControlType;
            set
            {
                selectedControlType = value;
                OnPropertyChanged();
            }
        }
        public string Name 
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RevitControl> Controls 
        {
            get => controls;
            set
            {
                controls = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RevitControl> NonSlideOuts => new ObservableCollection<RevitControl>(Controls.Where(x => !x.IsSlideOut));

        public RevitControl SelectedControl 
        {
            get => selectedControl;
            set
            {
                selectedControl = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddControlCommand { get; set; }
        public ICommand DeleteControlCommand { get; set; }
        //public ICommand SelectImageCommand { get; set; }
        public ICommand MoveCommand {  get; set; }
        //public ICommand EditItemsCommand { get; }
        public ICommand CloseCommand { get; }

        public PanelViewModel()
        {
            Controls = new ObservableCollection<RevitControl>();
            AddControlCommand = new AddControlCommand(this);
            //SelectImageCommand = new SelectImageCommand(this);
            DeleteControlCommand = new DeleteControlCommand(this);
            MoveCommand = new MoveCommand(this);
            //EditItemsCommand = new EditItemsCommand(this);
            CloseCommand = new CloseCommand(this);

            Test = new List<string>() { "1", "2" };
            AddingControls = new List<AddingControl>()
            {
                new AddingControl("Separator", ControlType.Separator),
                new AddingControl("RegularButton", ControlType.Regular),
                new AddingControl("PulldownButton", ControlType.Pulldown),
                new AddingControl("SplitButton", ControlType.SplitButton),
                new AddingControl("StackButton", ControlType.StackButton),
                new AddingControl("StackedPulldown", ControlType.StackedPulldown),
                new AddingControl("StackedSplitItem", ControlType.StackedSplitItem),
                new AddingControl("StackedRegularButton", ControlType.StackedRegButton),
                new AddingControl("TextBoxItem", ControlType.TextBox),
                new AddingControl("Label", ControlType.Textblock),
                new AddingControl("Checkbox", ControlType.Checkbox),
            };
        }
    }
}
