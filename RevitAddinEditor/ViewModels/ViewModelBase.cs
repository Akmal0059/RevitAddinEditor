using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitAddinEditor.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public DialogResult DialogResult { get; set; } = DialogResult.Cancel;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
