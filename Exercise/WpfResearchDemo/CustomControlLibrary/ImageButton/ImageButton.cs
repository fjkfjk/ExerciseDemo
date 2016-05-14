using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlLibrary
{
    public class ImageButton : Button
    {


        public string ImageText
        {
            get { return (string)GetValue(ImageTextProperty); }
            set { SetValue(ImageTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageTextProperty =
            DependencyProperty.Register("ImageText", typeof(string), typeof(ImageButton), new PropertyMetadata(null));



        public ImageSource NormalImage
        {
            get { return (ImageSource)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NormalImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton),
        new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

    }
}
