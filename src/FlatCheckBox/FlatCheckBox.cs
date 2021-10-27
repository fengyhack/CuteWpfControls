using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class FlatCheckBox : CheckBox
    {
        static FlatCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatCheckBox), new FrameworkPropertyMetadata(typeof(FlatCheckBox)));
        }

        public static readonly DependencyProperty UncheckedBackgroundProperty = DependencyProperty.Register("UncheckedBackground", typeof(Brush), typeof(FlatCheckBox),
            new PropertyMetadata(Brushes.DarkGray));

        public Brush UncheckedBackground
        {
            get { return (Brush)GetValue(UncheckedBackgroundProperty); }
            set { SetValue(UncheckedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CheckedBackgroundProperty = DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(FlatCheckBox),
            new PropertyMetadata(Brushes.WhiteSmoke));

        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty UncheckedColorProperty = DependencyProperty.Register("UncheckedColor", typeof(Brush), typeof(FlatCheckBox),
            new PropertyMetadata(Brushes.Crimson));

        public Brush UncheckedColor
        {
            get { return (Brush)GetValue(UncheckedColorProperty); }
            set { SetValue(UncheckedColorProperty, value); }
        }

        public static readonly DependencyProperty CheckedColorProperty = DependencyProperty.Register("CheckedColor", typeof(Brush), typeof(FlatCheckBox),
            new PropertyMetadata(Brushes.DodgerBlue));

        public Brush CheckedColor
        {
            get { return (Brush)GetValue(CheckedColorProperty); }
            set { SetValue(CheckedColorProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlatCheckBox));
    }
}
