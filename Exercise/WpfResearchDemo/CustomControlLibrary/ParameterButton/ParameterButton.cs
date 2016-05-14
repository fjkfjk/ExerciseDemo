using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomControlLibrary
{
    public class ParameterButton : Button
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ParameterButton), new PropertyMetadata(null));



        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(ParameterButton), new PropertyMetadata(null));



        public bool ShowUpDownArrow
        {
            get { return (bool)GetValue(ShowUpDownArrowProperty); }
            set { SetValue(ShowUpDownArrowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowUpDownArrow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowUpDownArrowProperty =
            DependencyProperty.Register("ShowUpDownArrow", typeof(bool), typeof(ParameterButton), new PropertyMetadata(false));



        static ParameterButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ParameterButton), new FrameworkPropertyMetadata(typeof(ParameterButton)));
        }
    }
}
