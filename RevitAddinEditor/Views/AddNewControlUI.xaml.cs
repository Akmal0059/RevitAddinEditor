using CustomRevitControls;
using RevitAddinEditor.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RevitAddinEditor.Views
{
    /// <summary>
    /// Interaction logic for AddNewControlUI.xaml
    /// </summary>
    public partial class AddNewControlUI : Window
    {
        public AddNewControlUI(IEnumerable<RevitControl> contols)
        {
            InitializeComponent();
            (DataContext as PanelViewModel).Controls = new ObservableCollection<RevitControl>(contols);
        }
    }
}
