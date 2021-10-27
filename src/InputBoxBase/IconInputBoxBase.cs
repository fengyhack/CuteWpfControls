using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace CuteWpfControls
{
    public class IconInputBoxBase : InputBoxBase
    {
        #region DependencyProperty

        #region IsIconVisible

        [Bindable(true), Description("IconVisibility")]
        public bool IsIconVisible
        {
            get { return (bool)GetValue(IsIconVisibleProperty); }
            set { SetValue(IsIconVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsIconVisibleProperty =
            DependencyProperty.Register("IsIconVisible", typeof(bool), typeof(IconInputBoxBase), new PropertyMetadata(true));

        #endregion

        #region IconBackground

        [Bindable(true), Description("IconBackground")]
        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register("IconBackground", typeof(Brush), typeof(IconInputBoxBase));

        #endregion

        #region IconForeground

        [Bindable(true), Description("IconColor")]
        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(IconInputBoxBase));

        #endregion

        #region IconBorderBrush

        [Bindable(true), Description("IconBorder")]
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

        [Bindable(true), Description("IconWidth")]
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(IconInputBoxBase));

        #endregion

        #region IconPadding

        [Bindable(true), Description("IconPadding")]
        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }

        public static readonly DependencyProperty IconPaddingProperty =
            DependencyProperty.Register("IconPadding", typeof(Thickness), typeof(IconInputBoxBase));

        #endregion

        #region IconCornerRadius

        [Bindable(true), Description("MaybeAuto")]
        public CornerRadius IconCornerRadius
        {
            get { return (CornerRadius)GetValue(IconCornerRadiusProperty); }
            set { SetValue(IconCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty IconCornerRadiusProperty =
            DependencyProperty.Register("IconCornerRadius", typeof(CornerRadius), typeof(IconInputBoxBase));

        #endregion

        #region IconPathData

        [Bindable(true), Description("Icon")]
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
