using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFhomework
{
    /// <summary>
    /// Interaction logic for SlideshowWindow.xaml
    /// </summary>
    public partial class SlideshowWindow : Window
    {
        public string folderPath { get; set; }
        int ctr = 0;
        public string[] _files;
        public int size;
        bool playing;
        DispatcherTimer timer;
        string effect;
        public SlideshowWindow(string tempPath, string _effect)
        {
            InitializeComponent();
            folderPath = tempPath;
            effect = _effect;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timer_Tick;
            timer.Start();
            playing = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ctr++;
            if (ctr == size)
                ctr = 0;
            PlaySlideShow(ctr);
        }
       

        private void PlaySlideShow(int ctr)
        {

            if (effect == "Horizontal Effect")
            {

                image1.Source = new BitmapImage(new Uri(_files[(ctr + 1) % size]));

                EasingDoubleKeyFrame f1 = new EasingDoubleKeyFrame();
                f1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
                f1.Value = 0;
                EasingDoubleKeyFrame f2 = new EasingDoubleKeyFrame();
                f2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000));
                f2.Value = 1024;
                DoubleAnimationUsingKeyFrames anima = new DoubleAnimationUsingKeyFrames();
                anima.KeyFrames.Add(f1);
                anima.KeyFrames.Add(f2);
                Storyboard.SetTargetName(anima, image1.Name);
                Storyboard.SetTargetProperty(anima, new PropertyPath(System.Windows.Controls.Image.WidthProperty));
                Storyboard HorAnimProp = new Storyboard();
                HorAnimProp.Children.Add(anima);
                
                image1.HorizontalAlignment = HorizontalAlignment.Left;

                image2.Source = new BitmapImage(new Uri(_files[ctr]));
                EasingDoubleKeyFrame g1 = new EasingDoubleKeyFrame();
                g1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
                g1.Value = 1024;
                EasingDoubleKeyFrame g2 = new EasingDoubleKeyFrame();
                g2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000));
                g2.Value = 0;
                DoubleAnimationUsingKeyFrames anima2 = new DoubleAnimationUsingKeyFrames();
                anima2.KeyFrames.Add(g1);
                anima2.KeyFrames.Add(g2);
                Storyboard.SetTargetName(anima2, image2.Name);
                Storyboard.SetTargetProperty(anima2, new PropertyPath(System.Windows.Controls.Image.WidthProperty));
                Storyboard HorAnimProp2 = new Storyboard();
                HorAnimProp2.Children.Add(anima2);
                image2.HorizontalAlignment = HorizontalAlignment.Right;


                HorAnimProp.Begin(image1);
                HorAnimProp2.Begin(image2);
            }
            if (effect == "Vertical Effect")
            {

                image1.Source = new BitmapImage(new Uri(_files[(ctr + 1) % size]));

                EasingDoubleKeyFrame f1 = new EasingDoubleKeyFrame();
                f1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
                f1.Value = 0;
                EasingDoubleKeyFrame f2 = new EasingDoubleKeyFrame();
                f2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000));
                f2.Value = 768;
                DoubleAnimationUsingKeyFrames anima = new DoubleAnimationUsingKeyFrames();
                anima.KeyFrames.Add(f1);
                anima.KeyFrames.Add(f2);
                Storyboard.SetTargetName(anima, image1.Name);
                Storyboard.SetTargetProperty(anima, new PropertyPath(System.Windows.Controls.Image.HeightProperty));
                Storyboard HorAnimProp = new Storyboard();
                HorAnimProp.Children.Add(anima);
                image1.VerticalAlignment = VerticalAlignment.Bottom;


                image2.Source = new BitmapImage(new Uri(_files[ctr]));
                EasingDoubleKeyFrame g1 = new EasingDoubleKeyFrame();
                g1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
                g1.Value = 768;
                EasingDoubleKeyFrame g2 = new EasingDoubleKeyFrame();
                g2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000));
                g2.Value = 0;
                DoubleAnimationUsingKeyFrames anima2 = new DoubleAnimationUsingKeyFrames();
                anima2.KeyFrames.Add(g1);
                anima2.KeyFrames.Add(g2);
                Storyboard.SetTargetName(anima2, image2.Name);
                Storyboard.SetTargetProperty(anima2, new PropertyPath(System.Windows.Controls.Image.HeightProperty));
                Storyboard HorAnimProp2 = new Storyboard();
                HorAnimProp2.Children.Add(anima2);
                image2.VerticalAlignment = VerticalAlignment.Top;

                HorAnimProp.Begin(image1);
                HorAnimProp2.Begin(image2);

                
            }
            if(effect == "Opacity Effect")
            {
                image1.Source = new BitmapImage(new Uri(_files[(ctr + 1) % size]));

                EasingDoubleKeyFrame f1 = new EasingDoubleKeyFrame();
                f1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
                f1.Value = 0;
                EasingDoubleKeyFrame f2 = new EasingDoubleKeyFrame();
                f2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000));
                f2.Value = 1;
                DoubleAnimationUsingKeyFrames anima = new DoubleAnimationUsingKeyFrames();
                anima.KeyFrames.Add(f1);
                anima.KeyFrames.Add(f2);
                Storyboard.SetTargetName(anima, image1.Name);
                Storyboard.SetTargetProperty(anima, new PropertyPath(System.Windows.Controls.Image.OpacityProperty));
                Storyboard HorAnimProp = new Storyboard();
                HorAnimProp.Children.Add(anima);
                image1.VerticalAlignment = VerticalAlignment.Bottom;


                image2.Source = new BitmapImage(new Uri(_files[ctr]));
                EasingDoubleKeyFrame g1 = new EasingDoubleKeyFrame();
                g1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
                g1.Value = 1;
                EasingDoubleKeyFrame g2 = new EasingDoubleKeyFrame();
                g2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000));
                g2.Value = 0;
                DoubleAnimationUsingKeyFrames anima2 = new DoubleAnimationUsingKeyFrames();
                anima2.KeyFrames.Add(g1);
                anima2.KeyFrames.Add(g2);
                Storyboard.SetTargetName(anima2, image2.Name);
                Storyboard.SetTargetProperty(anima2, new PropertyPath(System.Windows.Controls.Image.OpacityProperty));
                Storyboard HorAnimProp2 = new Storyboard();
                HorAnimProp2.Children.Add(anima2);
                image2.VerticalAlignment = VerticalAlignment.Top;

                HorAnimProp2.Begin(image2);
                HorAnimProp.Begin(image1);

            }
        }

        private void StopSlideshow(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            this.Close();
            
        }
        private void PlayStop(object sender, RoutedEventArgs e)
        {
            if (playing)
            {
                playing = false;
                timer.Stop();
            }
            else
            {
                playing = true;
                timer.Start();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (string file in Directory.EnumerateFiles(folderPath).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".bmp") || s.EndsWith(".gif")))
            {
                i++;
            }
            string[] files = new string[i];
            i = 0;
            foreach (string file in Directory.EnumerateFiles(folderPath).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".bmp") || s.EndsWith(".gif")))
            {
                files[i] = file;
                i++;
            }
            size = i;
            _files = files;
            string Path = files[0];
            BitmapImage image = new BitmapImage(new Uri(files[0]));
            image1.Source = image;
        }

        
    }
}
