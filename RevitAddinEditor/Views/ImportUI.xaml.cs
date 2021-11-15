using System;
using System.Collections.Generic;
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
    /// Interaction logic for ImportUI.xaml
    /// </summary>
    public partial class ImportUI : Window
    {
        public ImportUI()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml Files |*.xml";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TB_XMLFilePath.Text = dialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "resources Files |*.resx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TB_STRRESFilePath.Text = dialog.FileName;
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "resources Files |*.resx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TB_MEDIARESFilePath.Text = dialog.FileName;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_STRRESFilePath.Text) ||
                string.IsNullOrEmpty(TB_MEDIARESFilePath.Text) ||
                string.IsNullOrEmpty(TB_XMLFilePath.Text))
            {
                return;
            }
            DialogResult = true;
            Close();
        }

    }
}
