using CustomRevitControls;
using RevitAddinEditor.ViewModels;
using RevitAddinEditor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddinEditor.Commands
{
    public class OpenSlideOutCommand : CommandBase
    {
        EditorViewModel viewModel;

        public OpenSlideOutCommand(EditorViewModel vm)
        {
            viewModel = vm;
        }

        public override void Execute(object parameter)
        {
            var paramArray = parameter as object[];
            var btn = (System.Windows.Controls.Button)paramArray[0];
            var lb_panel = (System.Windows.Controls.ListBox)paramArray[1];
            Window win = Window.GetWindow(btn);

            Point startPoint = btn.TranslatePoint(new Point(0, 0), win);
            var p = win.PointToScreen(startPoint);


            SlideOutUI slideOut = new SlideOutUI(lb_panel);
            slideOut.Left = p.X - 2;
            slideOut.Top = p.Y;
            slideOut.Width = lb_panel.ActualWidth;
            var slideOutViewModel = slideOut.DataContext as SlideOutViewModel;
            slideOutViewModel.SelectedPanel = viewModel.SelectedPanel;
            slideOutViewModel.SlideOuts = new ObservableCollection<RevitControl>(viewModel.SelectedPanel.Controls.Where(x => x.IsSlideOut));
            slideOut.Show();
        }
    }
}
