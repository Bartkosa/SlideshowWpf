using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SlideshowEffect;

namespace WPFhomework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private object dummyNode = null;
        public string currPath;
        bool is_empty=true;
        Window[] windows;
        class PictureInfo
        {
            public string Name { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public long Size { get; set; }

            public string Path { get; set; }
        }



        public void LoadDll()
        {
            int i = 0;
            try
            {
                string plugName = ConfigurationSettings.AppSettings["Plugs"].ToString();
                var connectors = Directory.GetDirectories(plugName);
                foreach (var connect in connectors)
                {
                    string dllPath = GetPluggerDll(connect);
                    Assembly _Assembly = Assembly.LoadFile(dllPath);
                    var types = _Assembly.GetTypes()?.ToList();
                    var type = types?.Find(a => typeof(ISlideshowEffect).IsAssignableFrom(a));
                    var win = (ISlideshowEffect)Activator.CreateInstance(type);

                    

                    

                    MenuItem newMenuItem = new MenuItem
                    {
                        Header = win.Name,
                        FontSize = 16
                        
                    };
                    switch (win.Name)
                    {
                        case "Opacity Effect":
                            {
                                windows[0] = win.GetWindow(currPath);
                                newMenuItem.Click += ClickOpacity;
                                break;
                            }
                        case "Horizontal Effect":
                            {
                                windows[1] = win.GetWindow(currPath);
                                newMenuItem.Click += ClickHorizontal;
                                break;
                            }
                        case "Vertical Effect":
                            {
                                windows[2] = win.GetWindow(currPath);
                                newMenuItem.Click += ClickVertical;
                                break;
                            }
                    }

                    MenuSlide.Items.Add(newMenuItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Internal Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void ClickOpacity(object sender, RoutedEventArgs e)
        {
            windows[0].ShowDialog();
        }
        void ClickHorizontal(object sender, RoutedEventArgs e)
        {
            windows[1].ShowDialog();
        }
        void ClickVertical(object sender, RoutedEventArgs e)
        {
            windows[2].ShowDialog();
        }

        private string GetPluggerDll(string connect)
        {
            var files = Directory.GetFiles(connect, "*.dll");
            foreach (var file in files)
            {
                if (FileVersionInfo.GetVersionInfo(file).ProductName.StartsWith("Calci"))
                    return file;
            }
            return string.Empty;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string s in Environment.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);

            }
            TextBlock noFile = new TextBlock();
            noFile.Text = "No file selected!";
            noFile.VerticalAlignment = VerticalAlignment.Center;
            noFile.HorizontalAlignment = HorizontalAlignment.Center;
            noFile.Margin = new Thickness(10);
            stackInfo.Children.Add(noFile);
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }
        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = ((TreeViewItem)((TreeView)sender).SelectedItem);
            currPath = item.Tag.ToString();
            showPictures(item);

        }
        private void showPictures(TreeViewItem item)
        {
            this.DataContext = null;
            int i = 0;
            foreach (string file in Directory.EnumerateFiles(item.Tag.ToString()).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".bmp") || s.EndsWith(".gif")))
            {
                i++;
            }
            string[] files = new string[i];
            i = 0;
            foreach (string file in Directory.EnumerateFiles(item.Tag.ToString()).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".bmp") || s.EndsWith(".gif")))
            {
                files[i] = file;
                i++;
            }
            if (files.Length == 0)
                is_empty = true;
            else
                is_empty = false;
            var pictureTiles = files.Select(f =>
            {
                var length = new System.IO.FileInfo(f).Length;

                Bitmap image1 = new Bitmap(f, true);

                return new PictureInfo()
                {
                    Name = f.Substring(f.LastIndexOf("\\") + 1),
                    Size = length,
                    Height = image1.Height,
                    Width = image1.Width,
                    Path = f
                };
            });
            this.DataContext = pictureTiles;

        }
        private void picturesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((PictureInfo)((ListView)sender).SelectedItem);
            if (item == null)
            {
                stackInfo.Children.Clear();
                TextBlock noFile = new TextBlock();
                noFile.Text = "No file selected!";
                noFile.Margin = new Thickness(10);
                noFile.VerticalAlignment = VerticalAlignment.Center;
                noFile.HorizontalAlignment = HorizontalAlignment.Center;
                stackInfo.Children.Add(noFile);
            }
            else
            {
                stackInfo.Children.Clear();
                StackPanel stack1 = new StackPanel();
                stackInfo.Children.Add(stack1);
                StackPanel stack2 = new StackPanel();
                stackInfo.Children.Add(stack2);


                string[] names = new string[8];
                names[0] = "File name:";
                names[1] = item.Name;
                names[2] = "Width:";
                names[3] = item.Width.ToString();
                names[4] = "Height:";
                names[5] = item.Height.ToString();
                names[6] = "Size:";
                names[7] = item.Size.ToString();
                for (int i = 0; i < 8; i++)
                {
                    TextBlock temp = new TextBlock();
                    temp.Margin = new Thickness(5);
                    temp.Text = names[i];
                    if (i % 2 == 0)
                    {
                        temp.FontWeight = FontWeights.Bold;
                        stack1.Children.Add(temp);

                    }
                    else
                    {
                        stack2.Children.Add(temp);
                    }
                }
            }
        }
        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                
                string folder = dialog.SelectedPath;

                this.DataContext = null;
                int i = 0;
                foreach (string file in Directory.EnumerateFiles(folder).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".bmp") || s.EndsWith(".gif")))
                {
                    i++;
                }
                string[] files = new string[i];
                i = 0;
                foreach (string file in Directory.EnumerateFiles(folder).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".bmp") || s.EndsWith(".gif")))
                {
                    files[i] = file;
                    i++;
                }
                if (files.Length == 0)
                    is_empty = true;
                else
                    is_empty = false;
                var pictureTiles = files.Select(f =>
                {
                    var length = new System.IO.FileInfo(f).Length;

                    Bitmap image1 = new Bitmap(f, true);

                    return new PictureInfo()
                    {
                        Name = f.Substring(f.LastIndexOf("\\") + 1),
                        Size = length,
                        Height = image1.Height,
                        Width = image1.Width,
                        Path = f
                    };
                });
                this.DataContext = pictureTiles;
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About popup = new About();  
            popup.ShowDialog();
        }

       
       

        private void StartButton(object sender, RoutedEventArgs e)
        {
            if (is_empty == true)
            {
                MessageBox.Show("The selected folder does not contain any image!", "An error occured",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                
                   SlideshowWindow slideshow = new SlideshowWindow(currPath,comboBox.Text);
                   slideshow.ShowDialog();
                
            }
        }
        
    }
}
