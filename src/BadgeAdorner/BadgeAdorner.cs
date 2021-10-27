using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class BadgeAdorner : Adorner
    {
        private VisualCollection _Visuals;
        private readonly Grid _Grid;
        private readonly Border _Border;
        private readonly Label _Label;

        public BadgeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _Label = new Label()
            {
                //Style = (Style)TryFindResource("BadgeLabelStyle")
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
            };
            _Border = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Background = new SolidColorBrush(Colors.OrangeRed),
                BorderThickness = new Thickness(0),
                CornerRadius = new CornerRadius(1),
                Child = _Label
            };
            _Grid = new Grid();
            _Grid.Children.Add(_Border);
            _Label.DataContext = adornedElement;
            _Visuals = new VisualCollection(this) { _Grid };
        }

        protected override int VisualChildrenCount => _Visuals.Count;

        protected override Visual GetVisualChild(int index) => _Visuals[index];

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _Grid.Arrange(new Rect(-10, -10, finalSize.Width + 20, finalSize.Height + 20));
            return base.ArrangeOverride(finalSize);
        }

        public static readonly DependencyProperty BadgeTextProperty =
            DependencyProperty.RegisterAttached("BadgeText", typeof(string),
                typeof(BadgeAdorner), new PropertyMetadata("", BadgeTextChangedCallback));

        public static string GetBadgeText(DependencyObject d)
        {
            return (string)d.GetValue(BadgeTextProperty);
        }

        public static void SetBadgeText(DependencyObject d, string value)
        {
            d.SetValue(BadgeTextProperty, value);
        }

        private static void BadgeTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                BadgeAdorner adorner = (BadgeAdorner)element.GetOrAddAdorner(typeof(BadgeAdorner));
                adorner._Label.Content = e.NewValue?.ToString();
            }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius),
               typeof(BadgeAdorner), new PropertyMetadata(new CornerRadius(0), CornerRadiusChangedCallback));

        public static CornerRadius GetCornerRadius(DependencyObject d)
        {
            return (CornerRadius)d.GetValue(BadgeTextProperty);
        }

        public static void SetCornerRadius(DependencyObject d, CornerRadius value)
        {
            d.SetValue(BadgeTextProperty, value);
        }

        private static void CornerRadiusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                BadgeAdorner adorner = (BadgeAdorner)element.GetOrAddAdorner(typeof(BadgeAdorner));
                adorner._Border.CornerRadius = (CornerRadius)e.NewValue;
            }
        }

        public static readonly DependencyProperty BadgeAlignmentProperty =
            DependencyProperty.RegisterAttached("BadgeAlignment", typeof(BadgeAlignmentEnum),
                typeof(BadgeAdorner), new PropertyMetadata(BadgeAlignmentEnum.RightTop, BadgeAlignmentChangedCallback));

        public static BadgeAlignmentEnum GetBadgeAlignment(DependencyObject d)
        {
            return (BadgeAlignmentEnum)d.GetValue(BadgeAlignmentProperty);
        }

        public static void SetBadgeAlignment(DependencyObject d, BadgeAlignmentEnum value)
        {
            d.SetValue(BadgeAlignmentProperty, value);
        }

        private static void BadgeAlignmentChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                BadgeAdorner adorner = (BadgeAdorner)element.GetOrAddAdorner(typeof(BadgeAdorner));
                switch ((BadgeAlignmentEnum)e.NewValue)
                {
                    case BadgeAlignmentEnum.LeftTop:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Left;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case BadgeAlignmentEnum.CenterTop:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Center;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case BadgeAlignmentEnum.RightTop:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Right;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case BadgeAlignmentEnum.LeftCenter:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Left;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Center;
                        break;
                    case BadgeAlignmentEnum.CenterCenter:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Center;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Center;
                        break;
                    case BadgeAlignmentEnum.RightCenter:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Right;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Center;
                        break;
                    case BadgeAlignmentEnum.LeftBottom:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Left;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Bottom;
                        break;
                    case BadgeAlignmentEnum.CenterBottom:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Center;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Bottom;
                        break;
                    case BadgeAlignmentEnum.RightBottom:
                        adorner._Border.HorizontalAlignment = HorizontalAlignment.Right;
                        adorner._Border.VerticalAlignment = VerticalAlignment.Bottom;
                        break;
                    default:
                        break;
                }
            }
        }

        public static readonly DependencyProperty BadgeVisibilityProperty =
           DependencyProperty.RegisterAttached("BadgeVisibility", typeof(Visibility),
               typeof(BadgeAdorner), new PropertyMetadata(Visibility.Visible, BadgeVisibilityChangedCallback));

        public static Visibility GetBadgeVisibility(DependencyObject d)
        {
            return (Visibility)d.GetValue(BadgeVisibilityProperty);
        }

        public static void SetBadgeVisibility(DependencyObject d, Visibility value)
        {
            d.SetValue(BadgeVisibilityProperty, value);
        }

        private static void BadgeVisibilityChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                BadgeAdorner adorner = (BadgeAdorner)element.GetOrAddAdorner(typeof(BadgeAdorner));
                adorner._Label.Visibility = (Visibility)e.NewValue;
            }
        }

        public static readonly DependencyProperty BackgroundProperty =
           DependencyProperty.RegisterAttached("Background", typeof(SolidColorBrush),
               typeof(BadgeAdorner), new PropertyMetadata(new SolidColorBrush(Colors.OrangeRed), BackgroundChangedCallback));

        public static SolidColorBrush GetBackground(DependencyObject d)
        {
            return (SolidColorBrush)d.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject d, SolidColorBrush value)
        {
            d.SetValue(BackgroundProperty, value);
        }

        private static void BackgroundChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                BadgeAdorner adorner = (BadgeAdorner)element.GetOrAddAdorner(typeof(BadgeAdorner));
                adorner._Border.Background = (SolidColorBrush)e.NewValue;
            }
        }

        public static readonly DependencyProperty ForegroundProperty =
           DependencyProperty.RegisterAttached("Foreground", typeof(SolidColorBrush),
               typeof(BadgeAdorner), new PropertyMetadata(new SolidColorBrush(Colors.OrangeRed), ForegroundChangedCallback));

        public static SolidColorBrush GetForeground(DependencyObject d)
        {
            return (SolidColorBrush)d.GetValue(ForegroundProperty);
        }

        public static void SetForeground(DependencyObject d, SolidColorBrush value)
        {
            d.SetValue(ForegroundProperty, value);
        }

        private static void ForegroundChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                BadgeAdorner adorner = (BadgeAdorner)element.GetOrAddAdorner(typeof(BadgeAdorner));
                adorner._Label.Foreground = (SolidColorBrush)e.NewValue;
            }
        }
    }
}
