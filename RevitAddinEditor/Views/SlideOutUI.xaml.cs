using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RevitAddinEditor.Views
{
    /// <summary>
    /// Interaction logic for SlideOutUI.xaml
    /// </summary>
    public partial class SlideOutUI : Window
    {
        private delegate void CloseDelegate();
        bool alreadyClosed = false;
        ListBox lb_panel;

        public SlideOutUI(ListBox listBox)
        {
            InitializeComponent();
            lb_panel = listBox;
            Topmost = true;
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!alreadyClosed && !lb_panel.IsMouseOver)
            {
                alreadyClosed = true;
                Close();
            }
        }

        protected override async void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            await CloseAfterSeconds(0.5);
        }

        private async Task CloseAfterSeconds(double seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            if (!alreadyClosed && !lb_panel.IsMouseOver)
            {
                alreadyClosed = true;
                Close();
            }
        }
    }
}
