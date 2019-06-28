using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

using System.Windows.Media;

namespace CuteWpfControls
{
    /// <summary>
    /// 时间轴
    /// </summary>
    /// <remarks>2017-11-10</remarks>
    public class Timeline : ItemsControl
    {

        #region DependencyProperty

        #region UseDefaultSlotTemplate

        [Bindable(true), Description("是否使用默认时间点样式")]
        public bool UseDefaultSlotTemplate
        {
            get { return (bool)GetValue(UseDefaultSlotTemplateProperty); }
            set { SetValue(UseDefaultSlotTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty UseDefaultSlotTemplateProperty =
            DependencyProperty.Register("UseDefaultSlotTemplate", typeof(bool), typeof(Timeline), new PropertyMetadata(false));

        #endregion UseDefaultSlotTemplate

        #region DefaultFirstSlotTemplate

        [Bindable(true), Description("第一个时间轴点的默认样式")]
        public DataTemplate DefaultFirstSlotTemplate
        {
            get { return (DataTemplate)GetValue(DefaultFirstSlotTemplateProperty); }
            set { SetValue(DefaultFirstSlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultFirstSlotTemplateProperty =
            DependencyProperty.Register("DefaultFirstSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion DefaultFirstSlotTemplate

        #region DefaultMiddleSlotTemplate

        [Bindable(true), Description("中间的时间轴点的默认样式")]
        public DataTemplate DefaultMiddleSlotTemplate
        {
            get { return (DataTemplate)GetValue(DefaultMiddleSlotTemplateProperty); }
            set { SetValue(DefaultMiddleSlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultMiddleSlotTemplateProperty =
            DependencyProperty.Register("DefaultMiddleSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion DefaultMiddleSlotTemplate

        #region DefaultLastItemTemplate

        [Bindable(true), Description("最后一个时间轴点的默认样式")]
        public DataTemplate DefaultLastSlotTemplate
        {
            get { return (DataTemplate)GetValue(DefaultLastSlotTemplateProperty); }
            set { SetValue(DefaultLastSlotTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultLastSlotTemplateProperty =
            DependencyProperty.Register("DefaultLastSlotTemplate", typeof(DataTemplate), typeof(Timeline));

        #endregion DefaultLastItemTemplate

        #region UserDefinedSlotBrush

        [Bindable(true), Description("由用户自定义的时间轴点圈厚度。建议值1~7")]
        public double SlotBorderThickness
        {
            get { return (double)GetValue(SlotBorderThicknessProperty); }
            set { SetValue(SlotBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty SlotBorderThicknessProperty =
            DependencyProperty.Register("SlotBorderThickness", typeof(double), typeof(Timeline), new PropertyMetadata(2.0));

        [Bindable(true), Description("由用户自定义的时间轴点颜色（外圈画刷）。")]
        public Brush SlotOuterBrush
        {
            get { return (Brush)GetValue(SlotOuterBrushProperty); }
            set { SetValue(SlotOuterBrushProperty, value); }
        }

        public static readonly DependencyProperty SlotOuterBrushProperty =
            DependencyProperty.Register("SlotOuterBrush", typeof(Brush), typeof(Timeline), new PropertyMetadata(Brushes.LightBlue));

        [Bindable(true), Description("由用户自定义的时间轴点颜色（内圈画刷）。")]
        public Brush SlotInnerBrush
        {
            get { return (Brush)GetValue(SlotInnerBrushProperty); }
            set { SetValue(SlotInnerBrushProperty, value); }
        }

        public static readonly DependencyProperty SlotInnerBrushProperty =
            DependencyProperty.Register("SlotInnerBrush", typeof(Brush), typeof(Timeline), new PropertyMetadata(Brushes.Green));

        #endregion UserDefinedSlotBrush

        #region UserDefinedSlotTemplate

        [Bindable(true), Description("用户自定义的时间轴点的样式。")]
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

            if(timelineItem == null)
            {
                return;
            }

            if(index == 0)
            {
                timelineItem.IsFirstItem = true;
            }
            else if(index == Items.Count - 1)
            {
                timelineItem.IsLastItem = true;
            }
            else
            {
                timelineItem.IsMiddleItem = true;
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
                        //如果新添加项是放在第一位，则更改原来的第一位的属性值
                        SetTimelineItem(e.NewStartingIndex + e.NewItems.Count);
                    }
                    else if (e.NewStartingIndex == this.Items.Count - e.NewItems.Count)
                    {
                        //如果新添加项是放在最后一位，则更改原来的最后一位的属性值
                        SetTimelineItem(e.NewStartingIndex - 1);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if(e.OldStartingIndex == 0) 
                    {
                        //如果移除的是第一个，则更改更新后的第一项的属性值
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

        /// <summary>
        /// 设置TimelineItem的位置属性
        /// </summary>
        /// <param name="index"></param>
        private void SetTimelineItem(int index)
        {
            if(index > Items.Count || index < 0)
            {
                return;
            }

            TimelineItem timelineItem = ItemContainerGenerator.ContainerFromIndex(index) as TimelineItem;
            if(timelineItem == null)
            {
                return;
            }

            if (index == 0)
            {
                timelineItem.IsFirstItem = true;
            }
            else if (index == Items.Count - 1)
            {
                timelineItem.IsLastItem = true;
            }
            else
            {
                timelineItem.IsMiddleItem = true;
            }
        }

        #endregion PrivateFunction
    }
}
