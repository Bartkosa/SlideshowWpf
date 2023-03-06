using System;
using System.Windows;
using System.Windows.Controls;

namespace SlideshowEffect
{
    public interface ISlideshowEffect
    {
        string Name { get; }
        void PlaySlideshow(Image imageIn, Image imageOut, double windowWidth, double windowHeight);
        Window GetWindow(string path);

    }
}
