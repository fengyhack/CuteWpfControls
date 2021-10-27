using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CuteWpfControls
{
    [TemplatePart(Name = "ContentRoot", Type = typeof(FrameworkElement))]
    public class Flyout : ContentControl
    {
        #region DependencyProperty

        public FadePositionEnum FadePosition
        {
            get { return (FadePositionEnum)GetValue(FadePositionProperty); }
            set { SetValue(FadePositionProperty, value); }
        }

        public static readonly DependencyProperty FadePositionProperty =
            DependencyProperty.Register("FadePosition", typeof(FadePositionEnum), typeof(Flyout), new PropertyMetadata(FadePositionEnum.Top, OnFadePositionChanged));

        private static void OnFadePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout)d;
            var wasOpen = flyout.IsOpen;            

            if (wasOpen)
            {
                flyout.ApplyAnimation((FadePositionEnum)e.NewValue);
            }
            else
            {
                flyout.ApplyAnimation((FadePositionEnum)e.NewValue, false);
            }

            string visualStatus = GetFlyoutVisualStatus(wasOpen, flyout.IsAnimated);
            VisualStateManager.GoToState(flyout, visualStatus, true);
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Flyout), new PropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout)d;

            Action action = () => {                
                if (e.NewValue != e.OldValue)
                {
                    if ((bool)e.NewValue)
                    {
                        if (flyout.hideStoryboard != null)
                        {
                            flyout.hideStoryboard.Completed -= flyout.HideStoryboardCompleted;
                        }
                        flyout.Show();
                        flyout.ApplyAnimation(flyout.FadePosition);
                    }
                    else
                    {
                        if (flyout.hideStoryboard != null)
                        {
                            flyout.hideStoryboard.Completed += flyout.HideStoryboardCompleted;
                        }
                        else
                        {
                            flyout.Hide();
                        }
                    }
                }
                else
                {
                    if ((bool)e.NewValue)
                    {
                        flyout.Show();
                    }
                    else
                    {
                        flyout.Hide();                        
                    }
                }

                string visualStatus = GetFlyoutVisualStatus((bool)e.NewValue, flyout.IsAnimated);
                VisualStateManager.GoToState(flyout, visualStatus, true);
            };

            flyout.Dispatcher.BeginInvoke(DispatcherPriority.Background, action);
        }        

        public bool IsAnimated
        {
            get { return (bool)GetValue(IsAnimatedProperty); }
            set { SetValue(IsAnimatedProperty, value); }
        }

        public static readonly DependencyProperty IsAnimatedProperty =
            DependencyProperty.Register("IsAnimated", typeof(bool), typeof(Flyout), new PropertyMetadata(false));

        private static string GetFlyoutVisualStatus(bool isOpen, bool isAnimated)
        {
            return isOpen ? (isAnimated ? "ShowAnimated" : "ShowDirectly") :
                (isAnimated ? "HideAnimated" : "HideDirectly");
        }

        #endregion DependencyProperty

        #region PrivateMembers

        private static bool registered = false;
        private bool gotTemplate = false;
        private Storyboard showStoryboard;
        private SplineDoubleKeyFrame showFrameX;
        private SplineDoubleKeyFrame showFrameY;
        private Storyboard hideStoryboard;
        private SplineDoubleKeyFrame hideFrameX;
        private SplineDoubleKeyFrame hideFrameY;

        #endregion PrivateMembers

        #region Ctor

        public Flyout()
        {
            if (registered) return;
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Flyout), new FrameworkPropertyMetadata(typeof(Flyout)));
            registered = true;
        }

        #endregion Ctor

        #region PrivateFunctions

        private void GetTemplateItems()
        {
            if (gotTemplate) return;

            showStoryboard = GetTemplateChild("showStoryboard") as Storyboard;
            showFrameX = GetTemplateChild("showFrameX") as SplineDoubleKeyFrame;
            showFrameY = GetTemplateChild("showFrameY") as SplineDoubleKeyFrame;

            hideStoryboard = GetTemplateChild("hideStoryboard") as Storyboard;
            hideFrameX = GetTemplateChild("hideFrameX") as SplineDoubleKeyFrame;
            hideFrameY = GetTemplateChild("hideFrameY") as SplineDoubleKeyFrame;

            gotTemplate = true;
        }

        protected void ApplyAnimation(FadePositionEnum fadePosition, bool resetShowFrame = true)
        {
            if (!gotTemplate) return;

            if (fadePosition == FadePositionEnum.Left || fadePosition == FadePositionEnum.Right)
            {
                showFrameX.Value = 0;
            }
            else
            {
                showFrameY.Value = 0;
            }

            switch (fadePosition)
            {
                case FadePositionEnum.Left:
                    HorizontalAlignment = Margin.Right <= 0 ? (HorizontalContentAlignment != HorizontalAlignment.Stretch ? HorizontalAlignment.Left : HorizontalContentAlignment) : HorizontalAlignment.Stretch;
                    VerticalAlignment = VerticalAlignment.Stretch;
                    hideFrameX.Value = -ActualWidth - Margin.Left;
                    showFrameX.Value = ActualWidth + Margin.Left;
                    if (resetShowFrame)
                    {
                        RenderTransform = new TranslateTransform(-ActualWidth, 0);
                    }
                    break;
                case FadePositionEnum.Right:
                    HorizontalAlignment = Margin.Left <= 0 ? (HorizontalContentAlignment != HorizontalAlignment.Stretch ? HorizontalAlignment.Right : HorizontalContentAlignment) : HorizontalAlignment.Stretch;
                    VerticalAlignment = VerticalAlignment.Stretch;
                    hideFrameX.Value = ActualWidth + Margin.Right;
                    showFrameX.Value = -ActualWidth + Margin.Right;
                    if (resetShowFrame)
                    {
                        RenderTransform = new TranslateTransform(ActualWidth, 0);
                    }
                    break;
                case FadePositionEnum.Top:
                    HorizontalAlignment = HorizontalAlignment.Stretch;
                    VerticalAlignment = Margin.Bottom <= 0 ? (VerticalContentAlignment != VerticalAlignment.Stretch ? VerticalAlignment.Top : VerticalContentAlignment) : VerticalAlignment.Stretch;
                    hideFrameY.Value = -ActualHeight - 1 - Margin.Top;
                    showFrameY.Value = ActualHeight + 1 + Margin.Top;
                    if (resetShowFrame)
                    {
                        RenderTransform = new TranslateTransform(0, -ActualHeight - 1);
                    }
                    break;
                case FadePositionEnum.Bottom:
                    HorizontalAlignment = HorizontalAlignment.Stretch;
                    VerticalAlignment = Margin.Top <= 0 ? (VerticalContentAlignment != VerticalAlignment.Stretch ? VerticalAlignment.Bottom : VerticalContentAlignment) : VerticalAlignment.Stretch;
                    hideFrameY.Value = ActualHeight + Margin.Bottom;
                    showFrameY.Value = -ActualHeight + Margin.Bottom;
                    if (resetShowFrame)
                    {
                        RenderTransform = new TranslateTransform(0, ActualHeight);
                    }
                    break;
                default:
                    break;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GetTemplateItems();
            ApplyAnimation(FadePosition);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sci)
        {
            base.OnRenderSizeChanged(sci);

            if (!IsOpen)
            {
                return;
            }

            if (!sci.WidthChanged && !sci.HeightChanged)
            {
                return;
            }

            if (hideFrameX == null || showFrameX == null || hideFrameY == null || this.showFrameY == null)
            {
                return;
            }

            if (FadePosition == FadePositionEnum.Left || FadePosition == FadePositionEnum.Right)
            {
                showFrameX.Value = 0;
            }
            else
            {
                showFrameY.Value = 0;
            }

            switch (FadePosition)
            {
                case FadePositionEnum.Left:
                    hideFrameX.Value = -ActualWidth - Margin.Left;
                    break;
                case FadePositionEnum.Right:
                    hideFrameX.Value = ActualWidth + Margin.Right;
                    break;
                case FadePositionEnum.Top:
                    hideFrameY.Value = -ActualHeight - 1 - Margin.Top;
                    break;
                case FadePositionEnum.Bottom:
                    hideFrameY.Value = ActualHeight + Margin.Bottom;
                    break;
                default:
                    break;
            }
        }

        private void Show()
        {
            Visibility = Visibility.Visible;
        }

        private void Hide()
        {
            Visibility = Visibility.Hidden;
        }

        private void HideStoryboardCompleted(object sender, EventArgs e)
        {
            hideStoryboard.Completed -= HideStoryboardCompleted;
            Hide();
        }

        #endregion PrivateFunctions
    }
}
