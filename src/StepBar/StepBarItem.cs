using System.Windows;
using System.Windows.Controls;

namespace CuteWpfControls
{
    public class StepBarItem : ContentControl
    {
        public string Indicator
        {
            get { return (string)GetValue(IndicatorProperty); }
            private set { SetValue(IndicatorProperty, value); }
        }

        public static readonly DependencyProperty IndicatorProperty =
            DependencyProperty.Register("Indicator", typeof(string), typeof(StepBarItem));

        public EnumItemLocation Location
        {
            get { return (EnumItemLocation)GetValue(LocationProperty); }
            private set { SetValue(LocationProperty, value); }
        }

        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(EnumItemLocation), typeof(StepBarItem),
                new PropertyMetadata(EnumItemLocation.Unkown));

        public EnumItemRelative Relative
        {
            get { return (EnumItemRelative)GetValue(RelativeProperty); }
            private set { SetValue(RelativeProperty, value); }
        }

        public static readonly DependencyProperty RelativeProperty =
            DependencyProperty.Register("Relative", typeof(EnumItemRelative), typeof(StepBarItem),
                new PropertyMetadata(EnumItemRelative.Unkown));

        static StepBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepBarItem), new FrameworkPropertyMetadata(typeof(StepBarItem)));
        }

        public void SetIndicator(string indicator)
        {
            Indicator = indicator;
        }

        public void SetLocation(EnumItemLocation location)
        {
            Location = location;
        }

        public void SetRelative(EnumItemRelative relative)
        {
            Relative = relative;
        }
    }
}
