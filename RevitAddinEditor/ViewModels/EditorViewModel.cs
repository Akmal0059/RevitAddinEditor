using RevitAddinEditor.Commands;
using RevitAddinEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using CustomRevitControls;
using System.Collections.ObjectModel;
using Control = System.Windows.Controls.Control;
using RevitAddinEditor.Commands.EditItemsCommands;

namespace RevitAddinEditor.ViewModels
{
    public class EditorViewModel : ViewModelBase
    {
        List<string> revitItems;
        ObservableCollection<RevitPanel> panels;
        RevitPanel selectedPanel;

        public RevitPanel SelectedPanel
        {
            get => selectedPanel;
            set
            {
                selectedPanel = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RevitPanel> Panels
        {
            get => panels;
            set
            {
                panels = value;
                OnPropertyChanged();
            }
        }
        public List<string> RevitItems
        {
            get => revitItems;
            set
            {
                revitItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand SetAssemblyCommand { get; }
        public ICommand OpenItemsEditorCommand { get; }
        public ICommand ImportSettings { get; }
        public ICommand ExportSettings { get; }
        public ICommand TestCmd { get; }
        public ICommand AddPanelCommand { get; }
        public ICommand RemovePanelCommand { get; }
        public ICommand CreateResxFileCommand {  get; }
        public ICommand EditPanelCommand { get; }

        public EditorViewModel()
        {
            //SetAssemblyCommand = new SetAssemblyCommand(this);
            CreateResxFileCommand = new CreateResxFilesCommand(this);
            OpenItemsEditorCommand = new OpenItemsEditorCommand(this);
            ImportSettings = new ImportSettingsCommand(this);
            ExportSettings = new ExportSettingsCommand(this);
            AddPanelCommand = new AddPanelCommand(this);
            EditPanelCommand = new EditPanelCommand(this);
            RemovePanelCommand = new RemovePanelCommand(this);

            revitItems = new List<string>();
            Panels = new ObservableCollection<RevitPanel>();
        }
        ImageSource GetImageSource(string path)
        {
            var bitmap = new Bitmap(path);
            var imageSource =new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                imageSource.BeginInit();
                imageSource.StreamSource = memory;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
            }
            return imageSource;
        }
    }
}
