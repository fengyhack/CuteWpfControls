using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class BusyMask : Control
    {
        #region DependencyProperty

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty = 
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyMask), new PropertyMetadata(false, OnIsBusyChanged));

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BusyMask mask = d as BusyMask;
            mask.Visibility = mask.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BusyMask), new PropertyMetadata("Loding..."));

        public Brush IndicatorForeground
        {
            get { return (Brush)GetValue(IndicatorForegroundProperty); }
            set { SetValue(IndicatorForegroundProperty, value); }
        }

        public static readonly DependencyProperty IndicatorForegroundProperty =
            DependencyProperty.Register("IndicatorForeground", typeof(Brush), typeof(BusyMask), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 122, 204))));

        public IndicatorTypeEnum IndicatorType
        {
            get { return (IndicatorTypeEnum)GetValue(IndicatorTypeProperty); }
            set { SetValue(IndicatorTypeProperty, value); }
        }

        public static readonly DependencyProperty IndicatorTypeProperty =
            DependencyProperty.Register("IndicatorType", typeof(IndicatorTypeEnum), typeof(BusyMask), new PropertyMetadata(IndicatorTypeEnum.Normal));

        #endregion DependencyProperty

        #region Constructors

        static BusyMask()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyMask), new FrameworkPropertyMetadata(typeof(BusyMask)));
        }

        #endregion Constructors

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Visibility = IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion Override
    }
}
