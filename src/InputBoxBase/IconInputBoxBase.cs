using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class IconInputBoxBase:InputBoxBase
    {
        #region DependencyProperty

        #region IsIconVisible

        [Bindable(true), Description("是否显示图标")]
        public bool IsIconVisible
        {
            get { return (bool)GetValue(IsIconVisibleProperty); }
            set { SetValue(IsIconVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsIconVisibleProperty =
            DependencyProperty.Register("IsIconVisible", typeof(bool), typeof(IconInputBoxBase), new PropertyMetadata(true));

        #endregion

        #region IconBackground

        [Bindable(true), Description("图标边框背景色")]
        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register("IconBackground", typeof(Brush), typeof(IconInputBoxBase));

        #endregion

        #region IconForeground

        [Bindable(true), Description("图标的颜色")]
        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(IconInputBoxBase));

        #endregion

        #region IconBorderBrush

        [Bindable(true), Description("图标边框背景色")]
        public Brush IconBorderBrush
        {
            get { return (Brush)GetValue(IconBorderBrushProperty); }
            set { SetValue(IconBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty IconBorderBrushProperty =
            DependencyProperty.Register("IconBorderBrush", typeof(Brush), typeof(IconInputBoxBase));

        #endregion

        #region IconBorderThickness

        public Thickness IconBorderThickness
        {
            get { return (Thickness)GetValue(IconBorderThicknessProperty); }
            set { SetValue(IconBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty IconBorderThicknessProperty =
            DependencyProperty.Register("IconBorderThickness", typeof(Thickness), typeof(IconInputBoxBase));

        #endregion

        #region IconWidth

        [Bindable(true), Description("图标的大小")]
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(IconInputBoxBase));

        #endregion

        #region IconPadding

        /// <summary>
        /// 获取或者设置图标的内边距
        /// </summary>
        [Bindable(true), Description("图标的内边距")]
        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }

        public static readonly DependencyProperty IconPaddingProperty =
            DependencyProperty.Register("IconPadding", typeof(Thickness), typeof(IconInputBoxBase));

        #endregion

        #region IconCornerRadius

        [Bindable(true), Description("图标边框的圆角（可以不用手动设置，系统会根据密码框的圆角值自动设置该值）")]
        public CornerRadius IconCornerRadius
        {
            get { return (CornerRadius)GetValue(IconCornerRadiusProperty); }
            set { SetValue(IconCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty IconCornerRadiusProperty =
            DependencyProperty.Register("IconCornerRadius", typeof(CornerRadius), typeof(IconInputBoxBase));

        #endregion

        #region IconPathData

        [Bindable(true), Description("密码框图标")]
        public PathGeometry IconPathData
        {
            get { return (PathGeometry)GetValue(IconPathDataProperty); }
            set { SetValue(IconPathDataProperty, value); }
        }

        public static readonly DependencyProperty IconPathDataProperty =
            DependencyProperty.Register("IconPathData", typeof(PathGeometry), typeof(IconInputBoxBase));

        #endregion

        #endregion

        public override void OnCornerRadiusChanged(CornerRadius newValue)
        {
            SetValue(IconCornerRadiusProperty, new CornerRadius(newValue.TopLeft, 0, 0, newValue.BottomLeft));
        }
    }
}
