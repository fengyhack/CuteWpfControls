using System.Windows;
using System.Windows.Controls;

namespace CuteWpfControls
{
    public class LoadingIndicator : Control
    {
        private FrameworkElement PART_Root;

        #region DependencyProperty
        public bool IsActived
        {
            get { return (bool)GetValue(IsActivedProperty); }
            set { SetValue(IsActivedProperty, value); }
        }
        
        public static readonly DependencyProperty IsActivedProperty =
            DependencyProperty.Register("IsActived", typeof(bool), typeof(LoadingIndicator), new PropertyMetadata(true, OnIsActivedChangedCallback));

        private static void OnIsActivedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingIndicator indicator = d as LoadingIndicator;
            if(indicator.PART_Root == null)
            {
                return;
            }

            VisualStateManager.GoToElementState(indicator.PART_Root, (bool)e.NewValue ? "Active" : "Inactive", true);
        }

        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpeedRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(LoadingIndicator), new PropertyMetadata(1d, OnSpeedRatioChangedCallback));

        private static void OnSpeedRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingIndicator indicator = d as LoadingIndicator;
            if(indicator.PART_Root == null || !indicator.IsActived)
            {
                return;
            }
            indicator.SetSpeedRatio(indicator.PART_Root, indicator.SpeedRatio);
        }

        public IndicatorTypeEnum IndicatorType
        {
            get { return (IndicatorTypeEnum)GetValue(IndicatorTypeProperty); }
            set { SetValue(IndicatorTypeProperty, value); }
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
            this.PART_Root = this.GetTemplateChild("PART_Root") as FrameworkElement;
            if(this.PART_Root != null)
            {
                VisualStateManager.GoToElementState(this.PART_Root, this.IsActived ? "Active" : "Inactive", true);
                this.SetSpeedRatio(this.PART_Root, this.SpeedRatio);
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
