using System.Windows;
using System.Windows.Controls;

namespace CuteWpfControls
{
    public class LoadingIndicator : Control
    {
        private FrameworkElement PART_Root;

        #region DependencyProperty
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LoadingIndicator), new PropertyMetadata(true, OnIsActiveChangedCallback));

        private static void OnIsActiveChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingIndicator indicator = d as LoadingIndicator;
            if (indicator.PART_Root == null)
            {
                return;
            }

            VisualStateManager.GoToElementState(indicator.PART_Root, (bool)e.NewValue ? "Active" : "Inactive", true);
        }

        public double SpeedRatio
        {
            get => (double)GetValue(SpeedRatioProperty);
            set => SetValue(SpeedRatioProperty, value);
        }

        // Using a DependencyProperty as the backing store for SpeedRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(LoadingIndicator), new PropertyMetadata(1.0, OnSpeedRatioChangedCallback));

        private static void OnSpeedRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingIndicator indicator = d as LoadingIndicator;
            if (indicator.PART_Root == null || !indicator.IsActive)
            {
                return;
            }
            indicator.SetSpeedRatio(indicator.PART_Root, indicator.SpeedRatio);
        }

        public IndicatorTypeEnum IndicatorType
        {
            get => (IndicatorTypeEnum)GetValue(IndicatorTypeProperty);
            set => SetValue(IndicatorTypeProperty, value);
        }

        public static readonly DependencyProperty IndicatorTypeProperty =
            DependencyProperty.Register("IndicatorType", typeof(IndicatorTypeEnum), typeof(LoadingIndicator), new PropertyMetadata(IndicatorTypeEnum.DoubleRound));


        

        #endregion DependencyProperty

        #region Constructors

        static LoadingIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingIndicator), new FrameworkPropertyMetadata(typeof(LoadingIndicator)));
        }

        #endregion Constructors

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_Root = GetTemplateChild("PART_Root") as FrameworkElement;
            if (PART_Root != null)
            {
                VisualStateManager.GoToElementState(PART_Root, IsActive ? "Active" : "Inactive", true);
                SetSpeedRatio(PART_Root, SpeedRatio);
            }
        }

        #endregion Override

        #region Private

        private void SetSpeedRatio(FrameworkElement element, double speedRatio)
        {
            foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(element))
            {
                if (group.Name == "ActiveStates")
                {
                    foreach (VisualState state in group.States)
                    {
                        if (state.Name == "Active")
                        {
                            state.Storyboard.SetSpeedRatio(element, speedRatio);
                        }
                    }
                }
            }
        }

        #endregion Private
    }
}
