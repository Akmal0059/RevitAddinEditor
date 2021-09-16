using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CustomRevitControls;
using RevitAddinBase;

namespace RevitAddinEditor.Models
{
    public class RevitPanel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<RevitControl> controls;
        string name, id, text;

        public string Id
        {
            get => id;
            set
            {
                id = value;
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
        public string Text
        {
            get => text;
            set
            {
                text = value;
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
        //public string ClassName { get; set; }
        //public string AssemblyName { get; set; }
        //public string AvailabilityName { get; set; }

        public RevitPanel()
        {
            controls = new ObservableCollection<RevitControl>();
        }

        void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}