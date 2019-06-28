using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace CuteWpfControls
{
    /// <summary>
    /// 时间轴
    /// </summary>
    /// <remarks>2017-11-10</remarks>
    public class TimelineItem : ContentControl
    {
        #region DependencyProperty

        #region IsFirstItem

        [Bindable(true), Description("该项在列表中是否是第一个")]
        public bool IsFirstItem
        {
            get { return (bool)GetValue(IsFirstItemProperty); }
            set { SetValue(IsFirstItemProperty, value); }
        }
        
        public static readonly DependencyProperty IsFirstItemProperty =
            DependencyProperty.Register("IsFirstItem", typeof(bool), typeof(TimelineItem), new PropertyMetadata(false));

        #endregion IsFirstItem

        #region IsMiddleItem

        [Bindable(true), Description("该项在列表中是否是中间的一个")]
        public bool IsMiddleItem
        {
            get { return (bool)GetValue(IsMiddleItemProperty); }
            set { SetValue(IsMiddleItemProperty, value); }
        }

        public static readonly DependencyProperty IsMiddleItemProperty =
            DependencyProperty.Register("IsMiddleItem", typeof(bool), typeof(TimelineItem), new PropertyMetadata(false));

        #endregion IsMiddleItem

        #region IsLastItem

        [Bindable(true), Description("该项在列表中是否是最后一个")]
        public bool IsLastItem
        {
            get { return (bool)GetValue(IsLastItemProperty); }
            set { SetValue(IsLastItemProperty, value); }
        }
        
        public static readonly DependencyProperty IsLastItemProperty =
            DependencyProperty.Register("IsLastItem", typeof(bool), typeof(TimelineItem), new PropertyMetadata(false));

        #endregion IsLastItem

        #endregion DependencyProperty

        #region Constructors

        static TimelineItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineItem), new FrameworkPropertyMetadata(typeof(TimelineItem)));
        }

        #endregion Constructors
    }
}
