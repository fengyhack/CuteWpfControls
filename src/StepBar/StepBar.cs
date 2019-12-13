using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace CuteWpfControls
{
    public class StepBar : ItemsControl
    {
        public double ItemMinWidth
        {
            get { return (double)GetValue(ItemMinWidthProperty); }
            set { SetValue(ItemMinWidthProperty, value); }
        }

        public static readonly DependencyProperty ItemMinWidthProperty =
            DependencyProperty.Register("ItemMinWidth", typeof(double), typeof(StepBar), new PropertyMetadata(50.0));

        public int Current
        {
            get { return (int)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(int), typeof(StepBar),
                new PropertyMetadata(1, OnCurrentChanged, OnCurrentCoerced));

        private static object OnCurrentCoerced(DependencyObject d, object baseValue)
        {
            var stepBar = d as StepBar;
            if (stepBar.Items.Count < 1)
            {
                return 0;
            }

            int current = Convert.ToInt32(baseValue);
            if (current < 1)
            {
                return 0;
            }
            else if (current > stepBar.Items.Count)
            {
                return stepBar.Items.Count + 1;
            }
            else
            {
                return current;
            }
        }

        private static void OnCurrentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var stepBar = d as StepBar;
            if (stepBar.Items.Count < 1)
            {
                return;
            }
            var current = (int)(e.NewValue);
            UpdateCurrent(stepBar, current);
        }

        private static void UpdateCurrent(StepBar stepBar, int current)
        {
            var n = stepBar.Items.Count;
            if (n == 0)
            {
                return;
            }
            else if (n == 1)
            {
                var item = stepBar.Items[0] as StepBarItem;
                switch(current)
                {
                    case 0:
                        item.SetRelative(EnumItemRelative.After);
                        break;
                    case 1:
                        item.SetRelative(EnumItemRelative.Over);
                        break;
                    case 2:
                    default:
                        item.SetRelative(EnumItemRelative.Before);
                        break;
                }                
            }
            else if (n == 2)
            {
                var item1 = stepBar.Items[0] as StepBarItem;
                var item2 = stepBar.Items[1] as StepBarItem;

                switch (current)
                {
                    case 0:
                        item1.SetRelative(EnumItemRelative.After);
                        item2.SetRelative(EnumItemRelative.After);
                        break;
                    case 1:
                        item1.SetRelative(EnumItemRelative.Over);
                        item2.SetRelative(EnumItemRelative.After);
                        break;
                    case 2:
                        item1.SetRelative(EnumItemRelative.Before);
                        item2.SetRelative(EnumItemRelative.Over);
                        break;
                    case 3:
                    default:
                        item1.SetRelative(EnumItemRelative.Before);
                        item2.SetRelative(EnumItemRelative.Before);
                        break;
                }
            }
            else
            {
                var firstItem = stepBar.Items[0] as StepBarItem;
                if(current < 1)
                {
                    firstItem.SetRelative(EnumItemRelative.After);
                }
                else if (current == 1)
                {
                    firstItem.SetRelative(EnumItemRelative.Over);
                }
                else
                {
                    firstItem.SetRelative(EnumItemRelative.Before);
                }
                for (int i = 2; i < n; ++i)
                {
                    var item = stepBar.Items[i - 1] as StepBarItem;
                    if (i < current)
                    {
                        item.SetRelative(EnumItemRelative.Before);
                    }
                    else if (i == current)
                    {
                        item.SetRelative(EnumItemRelative.Over);
                    }
                    else
                    {
                        item.SetRelative(EnumItemRelative.After);
                    }
                }
                var lastItem = stepBar.Items[n - 1] as StepBarItem;
                if (current < n)
                {
                    lastItem.SetRelative(EnumItemRelative.After);
                }
                else if(current == n)
                {
                    lastItem.SetRelative(EnumItemRelative.Over);
                }
                else
                {
                    lastItem.SetRelative(EnumItemRelative.Before);
                }
            }
        }

        static StepBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepBar), new FrameworkPropertyMetadata(typeof(StepBar)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new StepBarItem();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            UpdateLocation();            
        }

        private void UpdateLocation()
        {
            var n = Items.Count;
            if (n == 0)
            {
                return;
            }
            else if (n == 1)
            {
                var item = Items[0] as StepBarItem;
                item.SetIndicator("1");
                item.SetLocation(EnumItemLocation.Last);
            }
            else if (n == 2)
            {
                var item1 = Items[0] as StepBarItem;
                item1.SetIndicator("1");
                item1.SetLocation(EnumItemLocation.First);

                var item2 = Items[1] as StepBarItem;
                item1.SetIndicator("2");
                item1.SetLocation(EnumItemLocation.Last);
            }
            else
            {
                var firstItem = Items[0] as StepBarItem;
                firstItem.SetIndicator("1");
                firstItem.SetLocation(EnumItemLocation.First);
                for (int i = 1; i < n - 1; ++i)
                {
                    var item = Items[i] as StepBarItem;
                    item.SetIndicator($"{i + 1}");
                    item.SetLocation(EnumItemLocation.Internal);
                }
                var lastItem = Items[n - 1] as StepBarItem;
                lastItem.SetIndicator($"{n}");
                lastItem.SetLocation(EnumItemLocation.Last);
            }

            if (Current < 1 || Current > n)
            {
                Current = 1;
            }
        }
    }
}
