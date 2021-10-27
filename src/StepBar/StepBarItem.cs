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

        public StepBarItemLocationEnum Location
        {
            get { return (StepBarItemLocationEnum)GetValue(LocationProperty); }
            private set { SetValue(LocationProperty, value); }
        }

        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(StepBarItemLocationEnum), typeof(StepBarItem),
                new PropertyMetadata(StepBarItemLocationEnum.Unkown));

        public StepBarItemRelativeEnum Relative
        {
            get { return (StepBarItemRelativeEnum)GetValue(RelativeProperty); }
            private set { SetValue(RelativeProperty, value); }
        }

        public static readonly DependencyProperty RelativeProperty =
            DependencyProperty.Register("Relative", typeof(StepBarItemRelativeEnum), typeof(StepBarItem),
                new PropertyMetadata(StepBarItemRelativeEnum.Unkown));

        static StepBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepBarItem), new FrameworkPropertyMetadata(typeof(StepBarItem)));
        }

        public void SetIndicator(string indicator)
        {
            Indicator = indicator;
        }

        public void SetLocation(StepBarItemLocationEnum location)
        {
            Location = location;
        }

        public void SetRelative(StepBarItemRelativeEnum relative)
        {
            Relative = relative;
        }
    }
}
