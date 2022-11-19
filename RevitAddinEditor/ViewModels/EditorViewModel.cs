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
using RevitAddinEditor.Commands.TabSettingsCommands;

namespace RevitAddinEditor.ViewModels
{
    public class EditorViewModel : ViewModelBase
    {
        List<string> singleCommands;
        List<string> comboBoxes;
        //ObservableCollection<RevitPanel> panels;
        RevitPanel selectedPanel;
        ObservableCollection<RevitTab> tabs;
        ObservableCollection<RevitControl> nonSlideOuts;
        RevitTab selectedTab;
        string asmPath;
        public RevitTab SelectedTab 
        {
            get => selectedTab;
            set
            {
                selectedTab = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RevitTab> Tabs 
        {
            get => tabs;
            set
            {
                tabs = value;
                OnPropertyChanged();
            }
        }
        public RevitPanel SelectedPanel
        {
            get => selectedPanel;
            set
            {
                selectedPanel = value;
                if (selectedPanel != null)
                    NonSlideOuts = new ObservableCollection<RevitControl>(selectedPanel.Controls.Where(x => !x.IsSlideOut));
                else 
                    NonSlideOuts = new ObservableCollection<RevitControl>();
                OnPropertyChanged();
            }
        }
        //public ObservableCollection<RevitPanel> Panels
        //{
        //    get => panels;
        //    set
        //    {
        //        panels = value;
        //        OnPropertyChanged();
        //    }
        //}
        public ObservableCollection<RevitControl> NonSlideOuts
        {
            get => nonSlideOuts;
            set
            {
                nonSlideOuts = value;
                OnPropertyChanged();
            }
        }
        public List<string> SingleCommands
        {
            get => singleCommands;
            set
            {
                singleCommands = value;
                OnPropertyChanged();
            }
        }
        public List<string> ComboBoxes
        {
            get => comboBoxes;
            set
            {
                comboBoxes = value;
                OnPropertyChanged();
            }
        }
        public string AssemblyPath 
        {
            get => asmPath;
            set
            {
                asmPath = value;
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
        public ICommand EditPanelCommand { get; }
        public ICommand CreateResxFileCommand {  get; }
        public ICommand OpenSlideOutCommand { get; }
        public ICommand AddTabCommand { get; }
        public ICommand RemoveTabCommand { get; }
        public ICommand EditTabCommand { get; }

        public EditorViewModel()
        {
            SetAssemblyCommand = new SetAssemblyCommand(this);
            CreateResxFileCommand = new CreateResxFilesCommand(this);
            OpenItemsEditorCommand = new OpenItemsEditorCommand(this);
            ImportSettings = new ImportSettingsCommand(this);
            ExportSettings = new ExportSettingsCommand(this);
            AddPanelCommand = new AddPanelCommand(this);
            EditPanelCommand = new EditPanelCommand(this);
            RemovePanelCommand = new RemovePanelCommand(this);
            OpenSlideOutCommand = new OpenSlideOutCommand(this);

            AddTabCommand = new AddTabCommand(this);
            EditTabCommand = new EditTabCommand(this);
            RemoveTabCommand = new RemoveTabCommand(this);

            singleCommands = new List<string>();
            comboBoxes = new List<string>();
            Tabs = new ObservableCollection<RevitTab>();
            nonSlideOuts = new ObservableCollection<RevitControl>();
            //Panels = new ObservableCollection<RevitPanel>();
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
