using System.Windows;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class ToggleCheckBox : FlatCheckBox
    {
        static ToggleCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleCheckBox), new FrameworkPropertyMetadata(typeof(ToggleCheckBox)));
        }

        public static readonly DependencyProperty UncheckedTextProperty = DependencyProperty.Register("UncheckedText", typeof(string), typeof(ToggleCheckBox),
            new PropertyMetadata("OFF"));

        public string UncheckedText
        {
            get { return (string)GetValue(UncheckedTextProperty); }
            set { SetValue(UncheckedTextProperty, value); }
        }

        public static readonly DependencyProperty CheckedTextProperty = DependencyProperty.Register("CheckedText", typeof(string), typeof(ToggleCheckBox),
            new PropertyMetadata("ON"));

        public string CheckedText
        {
            get { return (string)GetValue(CheckedTextProperty); }
            set { SetValue(CheckedTextProperty, value); }
        }
    }
}
