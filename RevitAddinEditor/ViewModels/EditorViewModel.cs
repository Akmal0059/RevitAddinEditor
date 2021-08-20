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
        Control selectedControl;
        ObservableCollection<System.Windows.Controls.Control> controls;

        public ObservableCollection<System.Windows.Controls.Control> Controls
        {
            get => controls;
            set
            {
                controls = value;
                OnPropertyChanged();
            }
        }
        public Control SelectedControl
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
        public ICommand TestCmd { get; }

        public EditorViewModel()
        {
            revitItems = new List<string>();
            SetAssemblyCommand = new SetAssemblyCommand(this);
            OpenItemsEditorCommand = new OpenItemsEditorCommand(this);
            //DeleteControlCommand = new DeleteControlCommand(this);
            TestCmd = new TestCommand(this);
            Controls = new ObservableCollection<System.Windows.Controls.Control>();
            //AddSplitItem();
            //AddPulldown();
            //AddStack();
        }

        void AddSplitItem()
        {
            SplitItem spb = new SplitItem();
            spb.Content = "SplitBtn";
            spb.CurrentIndex = 0;
            //SplitItem spb2 = new SplitItem();
            var items = new List<SplitButtonItem>();
            items.Add(new SplitButtonItem("111", @"F:\Apps\design.png"));
            items.Add(new SplitButtonItem("222", @"F:\Apps\protect.png"));
            items.Add(new SplitButtonItem("333", @"F:\Apps\design.png"));
            items.Add(new SplitButtonItem("444", @"F:\Apps\protect.png"));
            spb.Items = items;
            //spb2.Items = items;
            Controls.Add(spb);
            //Controls.Add(spb2);
        }
        void AddPulldown()
        {
            PulldownButton pb = new PulldownButton();
            pb.MainIcon = GetImageSource(@"F:\Apps\design.png");
            pb.Content = "Pulldown";
            var items = new List<PulldownItem>();
            items.Add(new PulldownItem("111", @"F:\Apps\design.png"));
            items.Add(new PulldownItem("222", @"F:\Apps\protect.png"));
            items.Add(new PulldownItem("333", @"F:\Apps\design.png"));
            items.Add(new PulldownItem("444", @"F:\Apps\protect.png"));
            pb.Items = items;
            Controls.Add(pb);
        }
        void AddStack()
        {
            StackButton sb = new StackButton();
            StackedPulldown pb = new StackedPulldown();
            pb.MainIcon = GetImageSource(@"F:\Apps\design.png");
            pb.Content = "StPulldown";
            var items = new List<PulldownItem>();
            items.Add(new PulldownItem("111", @"F:\Apps\design.png"));
            items.Add(new PulldownItem("2225555", @"F:\Apps\protect.png"));
            items.Add(new PulldownItem("333", @"F:\Apps\design.png"));
            items.Add(new PulldownItem("444", @"F:\Apps\protect.png"));
            pb.Items = items;
            pb.CalculateWidth();
            
            StackedSplitItem spb = new StackedSplitItem();
            spb.Content = "StSplitItem";
            //SplitItem spb2 = new SplitItem();
            var items2 = new List<SplitButtonItem>();
            items2.Add(new SplitButtonItem("111", @"F:\Apps\design.png"));
            items2.Add(new SplitButtonItem("222121321", @"F:\Apps\protect.png"));
            items2.Add(new SplitButtonItem("333", @"F:\Apps\design.png"));
            items2.Add(new SplitButtonItem("444", @"F:\Apps\protect.png"));
            spb.Items = items2;
            spb.CalculateWidth();

            StackItem si = new StackItem();
            si.MainIcon = GetImageSource(@"F:\Apps\protect.png");
            si.Content = "Stack btn";
            si.CalculateWidth();


            TextBoxItem tbi = new TextBoxItem();
            tbi.TextBoxWidth = 120;

            sb.Items = new List<System.Windows.Controls.Control> { pb, tbi, si };
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
