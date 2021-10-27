using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

using System.Windows.Media;

namespace CuteWpfControls
{
    /// <remarks>2017-11-10</remarks>
    public class Timeline : ItemsControl
    {

        #region DependencyProperty

        #region UseDefaultSlotTemplate

        [Bindable(true), Description("defaultStyle")]
        public bool UseDefaultSlotTemplate
        {
            get { return (bool)GetValue(UseDefaultSlotTemplateProperty); }
            set { SetValue(UseDefaultSlotTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty UseDefaultSlotTemplateProperty =
            DependencyProperty.Register("UseDefaultSlotTemplate", typeof(bool), typeof(Timeline), new PropertyMetadata(false));

        #endregion UseDefaultSlotTemplate

        [Bindable(true), Description("theOnly")]
        public DataTemplate DefaultOnlySlotTemplate
        {
            get { return (DataTemplate)GetValue(DefaultOnlySlotTemplateProperty); }
            set { SetValue(DefaultOnlySlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultOnlySlotTemplateProperty =
            DependencyProperty.Register("DefaultOnlySlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #region DefaultFirstSlotTemplate

        [Bindable(true), Description("theFirst")]
        public DataTemplate DefaultFirstSlotTemplate
        {
            get { return (DataTemplate)GetValue(DefaultFirstSlotTemplateProperty); }
            set { SetValue(DefaultFirstSlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultFirstSlotTemplateProperty =
            DependencyProperty.Register("DefaultFirstSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion DefaultFirstSlotTemplate

        #region DefaultMiddleSlotTemplate

        [Bindable(true), Description("Middle")]
        public DataTemplate DefaultMiddleSlotTemplate
        {
            get { return (DataTemplate)GetValue(DefaultMiddleSlotTemplateProperty); }
            set { SetValue(DefaultMiddleSlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultMiddleSlotTemplateProperty =
            DependencyProperty.Register("DefaultMiddleSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion DefaultMiddleSlotTemplate

        #region DefaultLastItemTemplate

        [Bindable(true), Description("theLast")]
        public DataTemplate DefaultLastSlotTemplate
        {
            get { return (DataTemplate)GetValue(DefaultLastSlotTemplateProperty); }
            set { SetValue(DefaultLastSlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultLastSlotTemplateProperty =
            DependencyProperty.Register("DefaultLastSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion DefaultLastItemTemplate

        #region UserDefinedSlotBrush

        [Bindable(true), Description("1~7")]
        public double SlotBorderThickness
        {
            get { return (double)GetValue(SlotBorderThicknessProperty); }
            set { SetValue(SlotBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty SlotBorderThicknessProperty =
            DependencyProperty.Register("SlotBorderThickness", typeof(double), typeof(Timeline), new PropertyMetadata(2.0));

        [Bindable(true), Description("outer")]
        public Brush SlotOuterBrush
        {
            get { return (Brush)GetValue(SlotOuterBrushProperty); }
            set { SetValue(SlotOuterBrushProperty, value); }
        }

        public static readonly DependencyProperty SlotOuterBrushProperty =
            DependencyProperty.Register("SlotOuterBrush", typeof(Brush), typeof(Timeline), new PropertyMetadata(Brushes.LightBlue));

        [Bindable(true), Description("inner")]
        public Brush SlotInnerBrush
        {
            get { return (Brush)GetValue(SlotInnerBrushProperty); }
            set { SetValue(SlotInnerBrushProperty, value); }
        }

        public static readonly DependencyProperty SlotInnerBrushProperty =
            DependencyProperty.Register("SlotInnerBrush", typeof(Brush), typeof(Timeline), new PropertyMetadata(Brushes.Green));

        #endregion UserDefinedSlotBrush

        #region UserDefinedSlotTemplate

        [Bindable(true), Description("slot")]
        public DataTemplate SlotTemplate
        {
            get { return (DataTemplate)GetValue(SlotTemplateProperty); }
            set { SetValue(SlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty SlotTemplateProperty =
            DependencyProperty.Register("SlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion UserDefinedSlotTemplate

        #endregion DependencyProperty

        #region Constructors

        static Timeline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(typeof(Timeline)));
        }

        #endregion Constructors

        #region Override

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            int index = ItemContainerGenerator.IndexFromContainer(element);
            TimelineItem timelineItem = element as TimelineItem;

            if (timelineItem == null || Items.Count == 0)
            {
                return;
            }

            if (Items.Count == 1)
            {
                timelineItem.IsOnlyItem = true;
                timelineItem.IsFirstItem = false;
                timelineItem.IsMiddleItem = false;
                timelineItem.IsLastItem = false;
            }
            else if (index == 0)
            {
                timelineItem.IsOnlyItem = false;
                timelineItem.IsFirstItem = true;
                timelineItem.IsMiddleItem = false;
                timelineItem.IsLastItem = false;
            }
            else if (index == Items.Count - 1)
            {
                timelineItem.IsOnlyItem = false;
                timelineItem.IsFirstItem = false;
                timelineItem.IsMiddleItem = false;
                timelineItem.IsLastItem = true;
            }
            else
            {
                timelineItem.IsOnlyItem = true;
                timelineItem.IsFirstItem = false;
                timelineItem.IsMiddleItem = true;
                timelineItem.IsLastItem = false;
            }

            base.PrepareContainerForItemOverride(timelineItem, item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TimelineItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex == 0) 
                    {
                        SetTimelineItem(e.NewStartingIndex + e.NewItems.Count);
                    }
                    else if (e.NewStartingIndex == Items.Count - e.NewItems.Count)
                    {
                        SetTimelineItem(e.NewStartingIndex - 1);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex == 0) 
                    {
                        SetTimelineItem(0);
                    }
                    else
                    {
                        SetTimelineItem(e.OldStartingIndex - 1);
                    }
                    break;
            }
        }
        #endregion Override

        #region PrivateFunction

        /// <param name="index"></param>
        private void SetTimelineItem(int index)
        {
            if (Items.Count == 0 || index > Items.Count || index < 0)
            {
                return;
            }

            TimelineItem timelineItem = ItemContainerGenerator.ContainerFromIndex(index) as TimelineItem;
            if (timelineItem == null)
            {
                return;
            }

            if (Items.Count == 1)
            {
                timelineItem.IsOnlyItem = true;
                timelineItem.IsFirstItem = false;
                timelineItem.IsMiddleItem = false;
                timelineItem.IsLastItem = false;               
            }
            else if (index == 0)
            {
                timelineItem.IsOnlyItem = false;
                timelineItem.IsFirstItem = true;
                timelineItem.IsMiddleItem = false;
                timelineItem.IsLastItem = false;                
            }
            else if (index == Items.Count - 1)
            {
                timelineItem.IsOnlyItem = false;
                timelineItem.IsFirstItem = false;
                timelineItem.IsMiddleItem = false;
                timelineItem.IsLastItem = true;              
            }
            else
            {
                timelineItem.IsOnlyItem = true;
                timelineItem.IsFirstItem = false;
                timelineItem.IsMiddleItem = true;
                timelineItem.IsLastItem = false;
            }
        }

        #endregion PrivateFunction
    }
}
