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

namespace RevitAddinEditor.ViewModels
{
    public class EditorViewModel : ViewModelBase
    {
        List<string> revitItems;
        List<RevitPanel> panels;
        List<SplitButtonItem> items;
        RevitControl selectedControl;
        ObservableCollection<RevitControl> controls;

        public ObservableCollection<RevitControl> Controls
        {
            get => controls;
            set
            {
                controls = value;
                OnPropertyChanged();
            }
        }
        public RevitControl SelectedControl
        {
            get => selectedControl;
            set
            {
                selectedControl = value;
                OnPropertyChanged();
            }
        }
        public RevitPanel SelectedPanel { get; set; }
        public List<RevitPanel> Panels
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
        public List<SplitButtonItem> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }
        public ICommand SetAssemblyCommand { get; }
        public ICommand OpenItemsEditorCommand { get; }
        public ICommand ImportSettings { get; }
        public ICommand ExportSettings { get; }
        public ICommand TestCmd { get; }

        public EditorViewModel()
        {
            revitItems = new List<string>();
            SetAssemblyCommand = new SetAssemblyCommand(this);
            OpenItemsEditorCommand = new OpenItemsEditorCommand(this);
            //DeleteControlCommand = new DeleteControlCommand(this);
            TestCmd = new TestCommand(this);
            ImportSettings = new ImportSettingsCommand(this);
            ExportSettings = new ExportSettingsCommand(this);
            Controls = new ObservableCollection<RevitControl>();
            //AddSplitItem();
            //AddPulldown();
            AddStack();
        }

        void AddSplitItem()
        {
            //SplitItem spb = new SplitItem();
            //spb.Content = "SplitBtn";
            //spb.CurrentIndex = 0;
            ////SplitItem spb2 = new SplitItem();
            //var items = new List<SplitButtonItem>();
            //items.Add(new SplitButtonItem("111", @"F:\Apps\design.png"));
            //items.Add(new SplitButtonItem("222", @"F:\Apps\protect.png"));
            //items.Add(new SplitButtonItem("333", @"F:\Apps\design.png"));
            //items.Add(new SplitButtonItem("444", @"F:\Apps\protect.png"));
            //spb.Items = items;
            ////spb2.Items = items;
            //Controls.Add(spb);
            ////Controls.Add(spb2);
        }
        void AddPulldown()
        {
            //PulldownButton pb = new PulldownButton();
            //pb.MainIcon = GetImageSource(@"F:\Apps\design.png");
            //pb.Content = "Pulldown";
            //var items = new List<PulldownItem>();
            //items.Add(new PulldownItem("111", @"F:\Apps\design.png"));
            //items.Add(new PulldownItem("222", @"F:\Apps\protect.png"));
            //items.Add(new PulldownItem("333", @"F:\Apps\design.png"));
            //items.Add(new PulldownItem("444", @"F:\Apps\protect.png"));
            //pb.Items = items;
            //Controls.Add(pb);
        }
        void AddStack()
        {
            StackButton sb = new StackButton();
            sb.Content = "sb test";
            StackedPulldown pb = new StackedPulldown();
            //pb.MainIcon = GetImageSource(@"F:\Apps\design.png");
            pb.Content = "StPulldown";
            var items = new List<RevitControl>();
            items.Add(new StackedRegularButton() { Content = "123" });
            items.Add(new StackedRegularButton() { Content = "2225555" });
            items.Add(new StackedRegularButton() { Content = "333" });
            items.Add(new StackedRegularButton() { Content = "444" });
            pb.Items = items;
            pb.CalculateWidth();
            
            StackedSplitItem spb = new StackedSplitItem();
            spb.Content = "StSplitItem";
            //SplitItem spb2 = new SplitItem();
            var items2 = new List<RevitControl>();
            //items2.Add(new SplitButtonItem("111", @"F:\Apps\design.png"));
            //items2.Add(new SplitButtonItem("222121321", @"F:\Apps\protect.png"));
            //items2.Add(new SplitButtonItem("333", @"F:\Apps\design.png"));
            //items2.Add(new SplitButtonItem("444", @"F:\Apps\protect.png"));
            spb.Items = items2;
            spb.CalculateWidth();

            StackedRegularButton si = new StackedRegularButton() { Content = "Stack btn" };
            //si.MainIcon = GetImageSource(@"F:\Apps\protect.png");
            si.CalculateWidth();


            TextBoxItem tbi = new TextBoxItem();
            tbi.TextBoxWidth = 120;

            sb.Items = new List<RevitControl> { pb, spb, si };
            sb.Properties.Add(new PropertyItem(sb, "Content", new System.Windows.Controls.TextBox()));
            sb.Properties.Add(new PropertyItem(sb, "MainIcon", new System.Windows.Controls.TextBox()));
            sb.Properties.Add(new PropertyItem(sb, "Items", new System.Windows.Controls.Button(), new OpenItemsEditorCommand(this)));
            Controls.Add(sb);
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
